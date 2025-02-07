using System.Collections;
using UnityEngine;

namespace Project
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LevelLoader _levelLoader;

        private void Awake()
        {
            _levelLoader.Init();
        }
    }
}