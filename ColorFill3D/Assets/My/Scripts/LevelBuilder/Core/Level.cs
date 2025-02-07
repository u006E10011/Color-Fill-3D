using UnityEngine;

namespace Project
{
    public class Level : MonoBehaviour
    {
        private PlayerSpawnPoint _playerSpawnPoint;

        public PlayerSpawnPoint PlayerPosition
        {
            get
            {
                if (_playerSpawnPoint == null)
                {
                    if (Application.isEditor && Application.isPlaying)
                        Debug.LogError("Spawn Point is null");

                    return null;
                }

                return _playerSpawnPoint;
            }
            set
            {
                _playerSpawnPoint = value;
            }
        }
    }
}