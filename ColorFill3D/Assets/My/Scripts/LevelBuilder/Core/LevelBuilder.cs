using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Project.LevelBuilder
{
    public class LevelBuilder
    {
        public bool IsReplaced;

        private readonly Dictionary<Vector3, Item> _items = new();

        private Item _item;
        private ContainerItem _parent;
        private int _index;

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
            Object.DestroyImmediate(_parent.gameObject);
            _items.Clear();
        }

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

        private void Generate(Item prefab, Vector3 point)
        {
            var newItem = PrefabUtility.InstantiatePrefab(prefab, _parent.transform) as Item;
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

        private void ValiateParent()
        {
            if (_parent == null)
            {
                _parent = Object.FindAnyObjectByType<ContainerItem>(FindObjectsInactive.Include);

                if (_parent == null)
                    _parent = _parent != null ? _parent : new GameObject("Level").AddComponent<ContainerItem>();
            }
        }
    }
}