using UnityEngine;

namespace N19
{
    public class SoundControllerGlobal : SoundControllerInstance
    {
        private static SoundControllerGlobal _instance;
        public static SoundControllerGlobal Instance
        {
            get
            {
                if (_instance == null)
                {
                    _gameObject = new(nameof(SoundControllerGlobal));
                    _instance = _gameObject.AddComponent<SoundControllerGlobal>();
                }

                return _instance;
            }
        }

        private static GameObject _gameObject;

        private new void Awake()
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(_gameObject);
                base.Awake();

                return;
            }

            Destroy(gameObject);
        }
    }
}