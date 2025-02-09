using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project;
using UnityEngine;

namespace Project
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float _offset = 0.1f;
        [SerializeField] private float _offsetY = 5;

        [SerializeField, Space(10)] private float _smoothing = 3;

        [SerializeField] private float _minValueToCalculateRatio = 8;
        [SerializeField] private float _minFieldOfView = 50f;
        [SerializeField] private float _ratioFieldOfView = 3.823529f;
        [SerializeField] private Camera _camera;

        private void OnEnable() => EventBus.Instance.OnMoveCamera += MoveToTarget;
        private void OnDisable() => EventBus.Instance.OnMoveCamera -= MoveToTarget;

        public void MoveToTarget(Level level) => StartCoroutine(Follow(level));

        private IEnumerator Follow(Level level)
        {
            var size = CalculateSize(level, out var sortX, out var sortZ);
            var position = TargetPosition(size) + Vector3.forward * _offsetY;
            CalculateFiledOfView(sortX, sortZ);

            while (Vector3.Distance(transform.position, position) > _offset)
            {
                transform.position = Vector3.Lerp(transform.position, position, _smoothing * Time.deltaTime);

                yield return null;
            }
        }

        private Vector3 CalculateSize(Level level, out List<Transform> sortX, out List<Transform> sortZ)
        {
            List<Transform> item = level.gameObject.GetComponentsInChildren<Transform>().ToList();

            sortX = item.OrderBy(p => p.transform.position.x).ToList();
            sortZ = item.OrderBy(p => p.transform.position.z).ToList();

            return new()
            {
                x = sortX[0].position.x + sortX[^1].position.x,
                y = 0f,
                z = sortZ[0].position.z + sortZ[^1].position.z
            };
        }

        private Vector3 TargetPosition(Vector3 size) => new()
        {
            x = size.x / 2,
            y = transform.position.y,
            z = size.z / 2,
        };

        private void CalculateFiledOfView(List<Transform> sortX, List<Transform> sortZ)
        {
            var x = Mathf.Abs(sortX[0].localPosition.x) + sortX[^1].localPosition.x;
            var z = Mathf.Abs(sortZ[0].localPosition.z) + sortZ[^1].localPosition.z;

            var count = System.Math.Max(x, z);
            var value = count > _minValueToCalculateRatio ? _minFieldOfView + ((count  - _minValueToCalculateRatio) * _ratioFieldOfView) : _camera.fieldOfView;
            Debug.Log($"Count: {count} || Value: {value}");
            _camera.fieldOfView = Mathf.Clamp(value, _minFieldOfView, int.MaxValue);
        }
    }
}
