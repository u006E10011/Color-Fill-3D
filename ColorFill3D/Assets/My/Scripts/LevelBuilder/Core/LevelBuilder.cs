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

        private Item _item;
        private ContainerItem _container;
        private int _index;

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
                _container = Object.FindAnyObjectByType<ContainerItem>(FindObjectsInactive.Include);

                if (_container == null)
                    _container = _container != null ? _container : new GameObject("Level").AddComponent<ContainerItem>();
            }
        }
        #endregion

        #region Core
        public void SavePrefab(string path)
        {
            ValiateParent();
            var directoryPath = Path.GetDirectoryName(path);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            if (!path.EndsWith(".prefab"))
                path += ".prefab";

            PrefabUtility.SaveAsPrefabAssetAndConnect(_container.gameObject, path, InteractionMode.UserAction);
            AssetDatabase.Refresh();
        }

        private void Generate(Item prefab, Vector3 point)
        {
            var newItem = PrefabUtility.InstantiatePrefab(prefab, _container.transform) as Item;
            newItem.transform.position = point;
            newItem.name = $"{prefab.name} {_index}";

            _items.Add(point, newItem);
            _index++;
        }

        private void Destroy(Vector3 point)
        {
            if (_items.ContainsKey(point))
                _items.Remove(point);

            Object.DestroyImmediate(_item.gameObject);
        }
        #endregion
    }
}