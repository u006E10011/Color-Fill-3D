using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private LevelBuilderData _data;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _parent;

        [SerializeField, Space(10)] private float _sensitivityScrollCube = 15f;

        private Dictionary<Vector3, Cube> _point = new();
        private int _index;

        private void Update()
        {
            Add();
            Remove();
            SwitchCubeType();
        }

        #region GenerateCube
        private void Add()
        {
            if (GetPoint(out var point))
            {
                if (Validate(point) && Input.GetMouseButton(0))
                {
                    if (_parent == null)
                        _parent = new GameObject("Container Level").transform;

                    _point[point] = Instantiate(_data.Cube[_index], point, Quaternion.identity.normalized, _parent);
                }
            }
        }

        private void Remove()
        {
            if (GetPoint(out var point))
            {
                if (!Validate(point) && Input.GetMouseButton(1))
                {
                    Destroy(_point[point].gameObject);
                    _point.Remove(point);
                }
            }
        }

        private bool Validate(Vector3 position)
        {
            return !_point.ContainsKey(position);
        }

        private bool GetPoint(out Vector3 position)
        {
            var v = _camera.ScreenPointToRay(Input.mousePosition);
            //var result = Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo, Mathf.Infinity, _data.Layer);
            var result = Physics.Raycast(
                new Ray(new(v.origin.x, 1000, v.origin.z), v.direction),
                out var hitInfo, Mathf.Infinity, _data.Layer);

            position = new(Mathf.Round(hitInfo.point.x), _data.PositionY, Mathf.Round(hitInfo.point.z));
            return result;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (GetPoint(out var point))
            {
                if (Validate(point))
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(point, _data.Cube[_index].gameObject.transform.localScale);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(point, _data.Cube[_index].gameObject.transform.localScale);
                }
            }
        }
#endif
        #endregion

        private void SwitchCubeType()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > _sensitivityScrollCube)
                _index++;
            else if (Input.GetAxis("Mouse ScrollWheel") < _sensitivityScrollCube)
                _index--;

            if (_index + 1 > _data.Cube.Count)
                _index = 0;
            else if (_index < 0)
                _index = _data.Cube.Count - 1;
        }
    }
}