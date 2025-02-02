using UnityEngine;

namespace Project.LevelBuilder
{
    public class SceneModeController : MonoBehaviour
    {
        [Header("PlayMode")]
        [SerializeField] private Player _player;
        [SerializeField] private Camera _playerCamera;

        [Header("EditMode")]
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private Camera _levelBuilderCamera;
        [SerializeField] private Canvas _levelBuilderCanvas;

        private void Awake()
        {
            _player.gameObject.SetActive(true);
            _playerCamera.gameObject.SetActive(true);

            _levelBuilder.enabled = false;
            _levelBuilderCamera.gameObject.SetActive(false);
            _levelBuilderCanvas.gameObject.SetActive(false);
        }
    }
}