using System.Collections;
using UnityEngine;

namespace Project
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField, Space(10)] private float _smoothing = 3;
        [SerializeField] private float _offset = 0.1f;

        private void OnEnable() => EventBus.Instance.OnMoveCamera += MoveToTarget;
        private void OnDisable() => EventBus.Instance.OnMoveCamera -= MoveToTarget;

        private void MoveToTarget(Vector3 target) => StartCoroutine(Follow(target));

        private IEnumerator Follow(Vector3 target)
        {
            while(Vector3.Distance(transform.position, target) > _offset)
            {
                transform.position = Vector3.Lerp(transform.position, target, _smoothing * Time.deltaTime);

                yield return null;
            }
        }
    }
}
