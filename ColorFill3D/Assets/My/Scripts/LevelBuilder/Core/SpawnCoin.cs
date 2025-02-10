using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace Project.LevelBuilder
{
    public class SpawnCoin
    {
        public float OffsetY = .5f;

        private List<Coin> _items = new();
        private Transform _containerLevel;
        private Transform _containerCoin;

        public void Generate(Item prefab, List<Item> items)
        {
            if (!ContainerLevel())
                return;

            var index = 0;
            var cell = items.FindAll(p => p is Cell).ToList();
            _containerCoin = ContainerCoin();

            foreach (var point in cell)
            {
                var item = PrefabUtility.InstantiatePrefab(prefab, _containerCoin) as Coin;
                item.transform.position = point.transform.position + Vector3.up * OffsetY;
                item.name = $"Coint {index}";

                _items.Add(item);
            }
        }

        public void Clear()
        {
            if (_containerCoin == null)
                return;

            Undo.DestroyObjectImmediate(_containerCoin.gameObject);
            _items = new();
        }

        private Transform ContainerCoin()
        {
            var obj = GameObject.Find("Container Coin");
            _containerCoin = obj != null ? obj.transform : null;

            if (_containerCoin != null)
                Object.DestroyImmediate(_containerCoin.gameObject);

            var container = new GameObject("Container Coin").transform;
            container.parent = _containerLevel;
            Undo.RegisterCreatedObjectUndo(container.gameObject, container.gameObject.name);

            return container;
        }

        private bool ContainerLevel()
        {
            var container = Object.FindAnyObjectByType<Level>(FindObjectsInactive.Include);
            _containerLevel = container != null ? container.transform : null;

            return container;
        }
    }
}