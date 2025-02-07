using UnityEngine;

namespace Project
{
    public class GameController : MonoBehaviour
    {
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
            Debug.Log("Winner");

            EventBus.Instance.OnCompleteLevel?.Invoke();
            EventBus.Instance.OnUpdateProgress?.Invoke();
        }
    }
}