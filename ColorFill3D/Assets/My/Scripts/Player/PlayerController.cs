using UnityEngine;

namespace Project
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _sensitivity = 15f;

        [SerializeField, Space(10)] private float _offsetPositionToPoint = .5f;
        [SerializeField] private float _offset = .1f;

        private bool _isCompleteMove = true;

        private Vector3 _targetPosition;
        private Vector2 _startTouchPosition;

        private void Start() => _targetPosition = transform.position;

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

            if (_isCompleteMove)
            {
                Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, Mathf.Infinity, 1 << 9);
                _targetPosition = hitInfo.point - (direction * _offsetPositionToPoint);
            }

            return _targetPosition;
        }

        private void Move()
        {
            _isCompleteMove = Vector3.Distance(transform.position, _targetPosition) < _offset;

            if (!_isCompleteMove)
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_targetPosition, 0.3f);
        }
#endif
    }
}