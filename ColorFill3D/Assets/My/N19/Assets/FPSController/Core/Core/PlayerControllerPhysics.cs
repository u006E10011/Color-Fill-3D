using UnityEngine;

namespace N19
{
    [System.Serializable]
    public class PlayerControllerPhysics
    {
        public Transform IsGroundPoint;
        public bool IsGround { get; private set; }
        public Vector3 Velocity;

        private bool _isNotGround;
        private PlayerControllerData _data;

        public void Init(PlayerControllerData config) => _data = config;

        public void MathVelocity()
        {
            CheckGround();

            if (IsGround && Velocity.y < 0)
                Velocity.y = _data.Gravity;

            Velocity.y += Time.deltaTime * _data.Gravity;

            if (IsGround)
                Velocity.y = 0;
        }

        private void CheckGround()
        {
            _isNotGround = IsGround == false && Velocity.y < 0;

            IsGround = Physics.CheckSphere(IsGroundPoint.position, _data.Radius, _data.Ground);

            if (_isNotGround && IsGround)
                PlayerControllerEvent.OnJumpDown?.Invoke();
        }
    }
}