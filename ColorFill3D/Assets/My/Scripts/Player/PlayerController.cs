using UnityEngine;

namespace Project
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _sensitivity = 15f;

        [SerializeField, Space(10)] private float _offsetPositionToPoint = .5f;
        [SerializeField] private float _minDIrection = .6f;

        private Vector3 _direction;
        private Vector2 _startTouchPosition;

        private void Start() => _direction = transform.position;

        private void Update()
        {
            InputHandler();
            Move();
        }

        private void InputHandler()
        {
            if (Input.GetMouseButtonDown(0))
                _startTouchPosition = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                Vector2 currentTouchPosition = Input.mousePosition;
                Vector2 delta = currentTouchPosition - _startTouchPosition;

                if (delta.magnitude > _sensitivity)
                    GetDirection(delta);
            }
        }

        private Vector3 GetDirection(Vector2 delta)
        {
            Vector3 direction;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                direction = delta.x > 0 ? Vector3.right : Vector3.left;
            else
                direction = delta.y > 0 ? Vector3.forward : Vector3.back;

            Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, Mathf.Infinity, 1 << 8);

            if (Vector3.Distance(transform.position, hitInfo.point) > _minDIrection)
                _direction = hitInfo.point - (direction * _offsetPositionToPoint);

            return _direction;
        }

        private void Move()
        {
            if (Vector3.Distance(transform.position, _direction) > 0.1f)
                transform.position = Vector3.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, _direction.normalized * _minDIrection);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_direction, 0.3f);
        }
#endif
    }
}