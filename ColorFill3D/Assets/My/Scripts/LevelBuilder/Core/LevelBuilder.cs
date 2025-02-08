using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Project.LevelBuilder
{
    public class LevelBuilder
    {
        public bool IsReplaced;

        private readonly Dictionary<Vector3, Item> _items = new();

        private int _index;
        public string DirectoryPath = @"Assets/My/Prefab/Resources/";
        public string FileName = "Level";

        private Item _item;
        private Level _container;

        #region InteractableButton
        public void Remove()
        {
            if (GetPoint(out var point) && Exist())
                Destroy(point);
        }

        public void Add(Item prefab)
        {
            ValiateParent();

            if (GetPoint(out var point))
            {
                if (TryPlayerSpawnPoint(prefab, point))
                    return;

                if (Exist() && IsReplaced)
                {
                    Destroy(point);
                    Generate(prefab, point);
                }
                else if (!Exist() && !_items.ContainsKey(point))
                    Generate(prefab, point);
            }
        }

        public void Clear()
        {
            ValiateParent();
            Object.DestroyImmediate(_container.gameObject);
            _items.Clear();
        }
        #endregion

        #region Core
        public void SavePrefab()
        {
            ValiateParent();

            var path = DirectoryPath;
            var directoryPath = Path.GetDirectoryName(path);

            if (!path.EndsWith("/"))
                path += "/";

            path += _container.gameObject.name + ".prefab";

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            PrefabUtility.SaveAsPrefabAssetAndConnect(_container.gameObject, path, InteractionMode.UserAction);
            AssetDatabase.Refresh();
        }

        private void Generate(Item prefab, Vector3 point)
        {          
            var newItem = PrefabUtility.InstantiatePrefab(prefab, _container.transform) as Item;
            Undo.RegisterCreatedObjectUndo(newItem, $"Create {newItem.name}");

            newItem.transform.position = point;
            newItem.name = $"{prefab.name} {_index}";

            _items.Add(point, newItem);
            _index++;

            EditorUtility.SetDirty(newItem);
        }

        private bool TryPlayerSpawnPoint(Item prefab, Vector3 point)
        {
            if (prefab is PlayerSpawnPoint)
            {
                if (_container.PlayerPosition == null)
                {
                    _container.PlayerPosition = PrefabUtility.InstantiatePrefab(prefab, _container.transform) as PlayerSpawnPoint;
                    Undo.RegisterCreatedObjectUndo(_container.PlayerPosition, $"Create {_container.PlayerPosition.name}");
                }

                _container.PlayerPosition.SetPosition(point);
                _items[Vector3.up * 999] = _container.PlayerPosition;

                Undo.RegisterCompleteObjectUndo(_container.PlayerPosition, "Set new position PlayerSapwnPoint");

                return true;
            }

            return false;
        }

        private void Destroy(Vector3 point)
        {
            if (_items.ContainsKey(point))
                _items.Remove(point);

            Object.DestroyImmediate(_item.gameObject);
        }
        #endregion

        #region Auxiliary 
        public bool GetPoint(out Vector3 position)
        {
            var result = Physics.Raycast(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition), out var hitInfo, Mathf.Infinity);
            position = new(Mathf.Round(hitInfo.point.x), 0, Mathf.Round(hitInfo.point.z));

            if (hitInfo.collider.TryGetComponent<Item>(out var item))
                _item = item;
            else
                _item = null;

            return result;
        }

        public bool Exist()
        {
            return _item;
        }

        private void ValiateParent()
        {
            if (_container == null)
            {
                _container = Object.FindAnyObjectByType<Level>(FindObjectsInactive.Include);

                if (_container == null)
                    _container = _container != null ? _container : new GameObject(FileName + GetIndexLevel()).AddComponent<Level>();
            }
        }

        private string GetIndexLevel()
        {
            var path = Path.GetDirectoryName(DirectoryPath);
            return "_" + Directory.GetFiles(path, "*.prefab").Length.ToString();
        }
        #endregion

    }
}