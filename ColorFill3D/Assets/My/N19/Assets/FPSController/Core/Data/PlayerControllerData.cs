using UnityEngine;

namespace N19
{
    [CreateAssetMenu(fileName = nameof(PlayerControllerData), menuName = "N19/PlayerController/" + nameof(PlayerControllerData))]
    public class PlayerControllerData : ScriptableObject
    {
        public CameraType CameraType;
        public float SpeedRotationModel = 10;

        [Header("Stats")]
        public float Speed = 15;
        public float MinSpeed = 5;
        public float Acceleration = 25;
        public float JumpHeight = 2;

        [Space(5)]
        public bool IsEnableAcceleration = true;
        public bool PressingJump = true;

        [Header("Mouse")]
        public float Sensitivity = 200;
        public MinMax RotateMinMax = new(-90, 90);

        [Header("Physics")]
        public LayerMask Ground;
        public bool VisibleGizmos;
        public float Radius = 0.5f;
        public float Gravity = -9.8f;
    }
}