using System.Collections;
using UnityEngine;

using static Project.SwipeState;

namespace Project
{
    public class SwipeMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _offsetToCell = .55f;
        [SerializeField] private float _offsetToCellPositionY = .5f;

        private Vector3 _movementDirection;
        private Vector2 _startTouchPosition;

        private SwipeState _state = GetDirection;
        private Vector3 _cell;
        private Rigidbody _rb;
        private Coroutine _coroutine;

        private void Awake() => _rb = GetComponent<Rigidbody>();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _startTouchPosition = Input.mousePosition;

            if (Input.GetMouseButton(0) && _state == GetDirection)
            {
                Vector2 currentTouchPosition = Input.mousePosition;
                Vector2 swipeDelta = currentTouchPosition - _startTouchPosition;

                if (_coroutine == null && swipeDelta.magnitude > 50)
                    _coroutine = StartCoroutine(SetMovementDirection(swipeDelta));
            }
        }

        private void FixedUpdate() => _rb.velocity = _speed * Time.fixedDeltaTime * _movementDirection;
        private void OnTriggerEnter(Collider other) => _cell = Floor(other.transform.position, _offsetToCellPositionY);

        private IEnumerator SetMovementDirection(Vector2 swipeDelta)
        {
            while (_state == GetDirection)
            {
                if (Vector3.Distance(transform.position, _cell) < _offsetToCell)
                {
                    _state = SetDirection;
                    transform.position = _cell;
                }

                yield return null;
            }

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                _movementDirection = new Vector3(swipeDelta.x > 0 ? 1 : -1, 0, 0);
            else
                _movementDirection = new Vector3(0, 0, swipeDelta.y > 0 ? 1 : -1);

            _coroutine = null;
            _state = GetDirection;
        }

        private Vector3 Floor(Vector3 vector, float y) => new()
        {
            x = Mathf.FloorToInt(vector.x),
            y = y,
            z = Mathf.FloorToInt(vector.z)
        };
    }
}
