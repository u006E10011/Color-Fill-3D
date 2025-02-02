using UnityEngine;

namespace Project
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField, Space(10)] private float _smoothing = 3;
        [SerializeField] private float _radius;

        private void LateUpdate()
        {
            if (Vector3.Distance(transform.position, _target.position) > _radius)
                Follow();
        }

        private void Follow()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _smoothing * Time.deltaTime);
        }
    }
}
