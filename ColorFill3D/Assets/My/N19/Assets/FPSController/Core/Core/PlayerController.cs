using UnityEngine;
using YG;

using static N19.InputPlayer;

namespace N19
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IService
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _model;
        [SerializeField] private PlayerControllerData _data;

        private float _speed;
        private float _boostSpeed;

        [Space(10)] public PlayerControllerPhysics Physics;

        private CharacterController _controller;
        private PlayerControllerEventHandler _animation;
        private PlayerControllerCamera _rotationCamera;
        private PlayerControllerMove _direction;

        private void Awake()
        {
            _animation = new(Physics, _data);
            _rotationCamera = new(transform, _camera, _model, _data);
            _direction = new(transform, _camera, _data);

            _controller = GetComponent<CharacterController>();
        }

        private void Start() => Init();

        private void Update()
        {
            Physics.MathVelocity();
            _animation.Update();

            Move();
            _rotationCamera.RotateToDirection();

            if (!CursorController.Visible)
                _rotationCamera.OnRotation?.Invoke();

            Jump();
        }

        public void SetSensivity(float value)
        {
            _rotationCamera.SetSensivity(value);
            YandexGame.savesData.Sensivity = value;
            YandexGame.SaveProgress();
        }

        private void Init()
        {
            Physics.Init(_data);
            _rotationCamera.SetSensivity(YandexGame.savesData.Sensivity == 0 ? _data.Sensitivity : YandexGame.savesData.Sensivity);
        }

        private void Move()
        {
            _speed = _data.IsEnableAcceleration && Instance.IsAcceleration ? _data.Acceleration : _data.Speed;
            _controller.Move(_speed * Time.deltaTime * _direction.MoveDirection.Value());
        }

        private void Jump()
        {
            if (Physics.IsGround && Instance.IsJump)
                Physics.Velocity.y = Mathf.Sqrt(_data.JumpHeight * -2f * _data.Gravity);

            _controller.Move(Physics.Velocity * Time.deltaTime);
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying || !_data.VisibleGizmos)
                return;

            Gizmos.DrawRay(Physics.IsGroundPoint.position, Vector3.down * _data.JumpHeight);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Physics.IsGroundPoint.position, _data.Radius);
        }
#endif
    }
}