using UnityEngine;

public class SwipeMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private bool _isSwiping = false;

    private Vector3 _movementDirection;
    private Vector2 _startTouchPosition;
    private Rigidbody _rb;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPosition = Input.mousePosition;
            _isSwiping = true;
        }

        if (Input.GetMouseButton(0) && _isSwiping)
        {
            Vector2 currentTouchPosition = Input.mousePosition;
            Vector2 swipeDelta = currentTouchPosition - _startTouchPosition;

            if (swipeDelta.magnitude > 50)
            {
                SetMovementDirection(swipeDelta);
                _isSwiping = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
            _isSwiping = false;
    }


    private void FixedUpdate()
    {
        _rb.velocity = _speed * Time.fixedDeltaTime * _movementDirection;
    }

    private void SetMovementDirection(Vector2 swipeDelta)
    {
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            _movementDirection = new Vector3(swipeDelta.x > 0 ? 1 : -1, 0, 0);
        else
            _movementDirection = new Vector3(0, 0, swipeDelta.y > 0 ? 1 : -1);
    }
}
