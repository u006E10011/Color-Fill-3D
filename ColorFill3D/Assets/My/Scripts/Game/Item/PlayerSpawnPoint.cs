using UnityEngine;

namespace Project
{
    public class PlayerSpawnPoint : LevelBuilder.Item
    {
        [SerializeField] private float _offsetY = .5f;

        public Vector3 Point => transform.position + Vector3.up * _offsetY;

        public void SetPosition(Vector3 point)
        {
            transform.position = point;
        }
    }
}