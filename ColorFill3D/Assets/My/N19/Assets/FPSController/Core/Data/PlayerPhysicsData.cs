using UnityEngine;

namespace N19
{
    [CreateAssetMenu(fileName = nameof(PlayerPhysicsData), menuName = "N19/PlayerController/" + nameof(PlayerPhysicsData))]
    public class PlayerPhysicsData : ScriptableObject
    {
        public LayerMask Ground;
        public bool VisibleGizmos;
        public float Radius = 0.5f;
        public float Gravity = -9.8f;
    }
}