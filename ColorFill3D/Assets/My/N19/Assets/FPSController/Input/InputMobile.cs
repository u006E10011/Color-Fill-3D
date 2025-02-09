using UnityEngine;

namespace N19
{
    public class InputMobile : IInputPlayer
    {
        public InputMobile(Joystick joystick, SelectButton jump, Canvas canvas)
        {
            _move = joystick;
            _jump = jump;

            canvas.gameObject.Activate();
        }

        #region Propertis
        public bool IsJump { get; set; }
        public bool IsAcceleration { get; set; }

        public Vector2 MoveDirection { get; set; }
        public Vector2 MouseDirection { get; set; }
        #endregion

        #region Private
        private readonly Joystick _move;
        private readonly SelectButton _jump;
        #endregion

        public void Update()
        {
            MoveDirection = new Vector2(_move.Horizontal, _move.Vertical);
            IsJump = _jump.IsSelected;
        }

        public void Reset()
        {
            MoveDirection = new();
            MouseDirection = new();

            IsJump = false;
            IsAcceleration = false;
        }
    }
}