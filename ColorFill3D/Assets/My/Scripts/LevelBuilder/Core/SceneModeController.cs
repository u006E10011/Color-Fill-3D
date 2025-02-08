using UnityEngine;

namespace Project.LevelBuilder
{
    public class SceneModeController : MonoBehaviour
    {
        [Header("PlayMode")]
        [SerializeField] private Camera _playerCamera;

        [Header("EditMode")]
        [SerializeField] private DrawGizmos _drawGirmos;

        private void Awake()
        {
            _playerCamera.gameObject.SetActive(true);
            _drawGirmos.gameObject.SetActive(false);
        }
    }
}