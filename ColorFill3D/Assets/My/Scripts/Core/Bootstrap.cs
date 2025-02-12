using UnityEngine;

namespace Project
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private CameraFollower _camera;
        [SerializeField] private PlayerController _playerController;

        private void Awake()
        {
            N19.Initializer.Instance.Init();

            _levelLoader.Init();
            _camera.MoveToTarget(_levelLoader.CurrentLevel);
            Instantiate(_playerController, _levelLoader.CurrentLevel.PlayerPosition.Point, Quaternion.identity);
        }
    }
}