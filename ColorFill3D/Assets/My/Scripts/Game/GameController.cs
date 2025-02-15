using UnityEngine;

namespace Project
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private AudioClip _winClip;

        private void OnEnable()
        {
            EventBus.Instance.OnUpdateProgress += TryWinGame;
        }

        private void OnDisable()
        {
            EventBus.Instance.OnUpdateProgress -= TryWinGame;
        }

        private void TryWinGame()
        {
            if (Brush.Count() == 0)
                Win(); 
        }

        private void Win()
        {
            N19.SoundControllerGlobal.Instance.PlaySFX(_winClip);
            EventBus.Instance.OnCompleteLevel?.Invoke();
        }
    }
}