using System;
using UnityEngine;

namespace Project
{
    public class GameController : MonoBehaviour
    {
        public static Action OnUpdateProgress;


        private void OnEnable()
        {
            OnUpdateProgress += TryWinGame;
        }

        private void OnDisable()
        {
            OnUpdateProgress -= TryWinGame;
        }

        private void TryWinGame()
        {
            if (Brush.Count() == 0)
                Win();
        }

        private void Win()
        {
            Debug.Log("Winner");
        }
    }
}