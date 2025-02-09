using UnityEngine;

namespace N19
{
    [CreateAssetMenu(fileName = nameof(PlayerStatsData), menuName = "N19/PlayerController/" + nameof(PlayerStatsData))]
    public class PlayerStatsData : ScriptableObject
    {
        public float Speed = 15;
        public float MinSpeed = 5;
        public float Acceleration = 25;
        public float JumpHeight = 2;

        [Space(5)]
        public bool IsEnableAcceleration = true;
        public bool PressingJump = true;
    }
}