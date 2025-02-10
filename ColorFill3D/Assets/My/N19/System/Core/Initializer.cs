using UnityEngine;

namespace N19
{
    public class Initializer : MonoBehaviour
    {
        private static GameObject _gameObject;
        private static Initializer _instance;
        public static Initializer Instance
        {
            get
            {
                if(_instance == null)
                {
                    _gameObject = new(nameof(Initializer));
                    _instance = _gameObject.AddComponent<Initializer>();
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(_gameObject);
                return;
            }

            Destroy(gameObject);
        }

        public void Init()
        {
            SetTargetFrame(DataProvider.Data.TargetFrameRate);
            CursorController.IsVisible(DataProvider.Data.VisibleCursor);
        }

        public void SetTargetFrame(int frame)
        {
            Application.targetFrameRate = frame;
        }
    }
}