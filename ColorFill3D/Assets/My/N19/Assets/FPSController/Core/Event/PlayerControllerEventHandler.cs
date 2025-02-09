using static N19.InputPlayer;
using static N19.PlayerControllerEvent;

namespace N19
{
    public class PlayerControllerEventHandler
    {
       public PlayerControllerEventHandler(PlayerControllerPhysics physics, PlayerControllerData data)
        {
            _physics = physics;
            _data = data;
        }

        private readonly PlayerControllerData _data;
        private readonly PlayerControllerPhysics _physics;

        public void Update()
        {
            Move();
            IsJump();
            IsFly();
        }

        private void Move()
        {
            var isWalk = Instance.MoveDirection.x == 0 && Instance.MoveDirection.y == 0;

            if (!isWalk && _physics.IsGround)
            {
                var isRunning = _data.IsEnableAcceleration && Instance.IsAcceleration ? OnRunning : OnWalk;
                isRunning?.Invoke();
            }
            else if (isWalk)
                OnIdle?.Invoke();
        }

        private void IsJump()
        {
            if (_physics.IsGround && Instance.IsJump)
                OnJumpUp?.Invoke();
        }

        private void IsFly()
        {
            //if (!_physics.IsGround)
            //    OnFly?.Invoke();
        }
    }
}