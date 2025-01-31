using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private Cell _cellPrefab;

        [SerializeField, Space(10)] private Camera _camera;
        [SerializeField] private Transform _parentCube;
        [SerializeField] private Transform _parentCell;

        [SerializeField, Space(10)] private int _layer;
        [SerializeField] private float _positionY;

        private readonly Dictionary<Vector3, Item> _items = new();

        private void Update()
        {
            DrawCell();
            DrawCube();
        }

        #region GenerateCube
        private void DrawCell()
        {
            if (Input.GetMouseButton(0) && GetPoint(out var point))
            {
                if (Exist(point))
                {
                    Destroy(_items[point].gameObject);
                    _items.Remove(point);
                }

                if (_parentCell == null)
                    _parentCell = new GameObject("Container Cell").transform;

                _items[point] = Instantiate(_cellPrefab, point, _cellPrefab.transform.rotation, _parentCell);
            }
        }

        private void DrawCube()
        {
            if (Input.GetMouseButton(1) && GetPoint(out var point))
            {
                if (Exist(point))
                {
                    Destroy(_items[point].gameObject);
                    _items.Remove(point);
                }

                if (_parentCube == null)
                    _parentCube = new GameObject("Container Cube").transform;

                _items[point] = Instantiate(_cubePrefab, point, Quaternion.identity.normalized, _parentCube);
            }
        }

        private bool Exist(Vector3 position)
        {
            return _items.ContainsKey(position);
        }

        private bool GetPoint(out Vector3 position)
        {
            var result = Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo, Mathf.Infinity);
            position = new(Mathf.Round(hitInfo.point.x), _positionY, Mathf.Round(hitInfo.point.z));
            return result;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (GetPoint(out var point))
            {
                if (!Exist(point))
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(point, _cubePrefab.gameObject.transform.localScale);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(point, _cubePrefab.gameObject.transform.localScale);
                }
            }
        }
#endif
        #endregion
    }
}