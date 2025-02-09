using UnityEngine;

namespace N19
{
    public class PlayerControllerMove
    {
        public PlayerControllerMove(Transform owner, Transform camera, PlayerControllerData data)
        {
            MoveDirection = data.CameraType switch
            {
                CameraType.FPS => new DirectionOwnerForward(owner),
                CameraType.TPS_V1 => new DirectionCameraForward(camera),
                CameraType.TPS_V2 => new DirectionCameraForward(camera),
                _ => new DirectionOwnerForward(owner)
            };
        }

        public IMoveDirection MoveDirection;

        private float _speed;
    }
}