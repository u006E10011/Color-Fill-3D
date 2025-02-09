using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float _offset = 0.1f;
        [SerializeField] private float _offsetY = 5;

        [SerializeField, Space(10)] private float _smoothing = 3;

        [SerializeField] private float _minOrthographicSize = 5.35f;
        [SerializeField] private float _ratioSize = .25f;
        [SerializeField] private float _minCountCubeLineToCalculateSize = 8;

        [SerializeField] private Camera _camera;

        private float _currentCountCubeLine;

        private void OnEnable() => EventBus.Instance.OnMoveCamera += MoveToTarget;
        private void OnDisable() => EventBus.Instance.OnMoveCamera -= MoveToTarget;

        public void MoveToTarget(Level level) => StartCoroutine(Follow(level));

        private IEnumerator Follow(Level level)
        {
            var position = CalculatePosition(level, out var sortX, out var sortZ) + Vector3.forward * _offsetY;
            CameraSize(sortX, sortZ);

            while (Vector3.Distance(transform.position, position) > _offset)
            {
                transform.position = Vector3.Lerp(transform.position, position, _smoothing * Time.deltaTime);

                yield return null;
            }
        }

        private Vector3 CalculatePosition(Level level, out List<Transform> sortX, out List<Transform> sortZ)
        {
            List<Transform> item = level.gameObject.GetComponentsInChildren<Transform>().ToList();

            sortX = item.OrderBy(p => p.transform.position.x).ToList();
            sortZ = item.OrderBy(p => p.transform.position.z).ToList();

            var x = sortX[0].position.x + sortX[^1].position.x;
            var z = sortZ[0].position.z + sortZ[^1].position.z;

            return new()
            {
                x = x / 2,
                y = transform.position.y,
                z = z / 2,
            };
        }

        private void CameraSize(List<Transform> sortX, List<Transform> sortZ)
        {
            var x = Mathf.Abs(sortX[0].localPosition.x) + sortX[^1].localPosition.x;
            var z = Mathf.Abs(sortZ[0].localPosition.z) + sortZ[^1].localPosition.z;

            _currentCountCubeLine = System.Math.Max(x, z);

            float value = 0;

            if (_currentCountCubeLine > _minCountCubeLineToCalculateSize)
                value = _ratioSize * (_currentCountCubeLine - _minCountCubeLineToCalculateSize);

            _camera.orthographicSize = Mathf.Clamp(_minOrthographicSize + value, _minOrthographicSize, int.MaxValue);
        }
    }
}
