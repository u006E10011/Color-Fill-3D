using UnityEngine;
using System.Collections.Generic;

namespace Project.LevelBuilder
{
    public class SpawnCoin
    {
#if UNITY_EDITOR
        public float OffsetY = .5f;

        private List<Coin> _items = new();
        private Transform _containerLevel;

        public void Generate(Item prefab)
        {
            if (!ContainerLevel())
                return;

            var index = 0;

            foreach (var point in FindAllCell())
            {
                var item = UnityEditor.PrefabUtility.InstantiatePrefab(prefab, _containerLevel) as Coin;
                item.transform.position = point.transform.position + Vector3.up * OffsetY;
                item.name = $"Coint {index}";

                _items.Add(item);
            }
        }

        public void Clear()
        {
            if (_containerLevel == null)
                return;

            UnityEditor.Undo.DestroyObjectImmediate(_containerLevel.gameObject);
            _items = new();
        }

        private List<Cell> FindAllCell()
        {
            var cell = new List<Cell>();

            foreach (Transform item in _containerLevel)
            {
                if (item.gameObject.TryGetComponent<Cell>(out var Item))
                    cell.Add(Item);
            }

            return cell;
        }


        private bool ContainerLevel()
        {
            var container = Object.FindAnyObjectByType<Level>(FindObjectsInactive.Include);
            _containerLevel = container != null ? container.transform : null;

            return container;
        }
#endif
    }
}