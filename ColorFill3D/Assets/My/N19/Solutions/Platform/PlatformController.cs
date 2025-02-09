using UnityEngine;
#if YG_PLUGIN_YANDEX_GAME
using YG;
#endif

namespace N19
{
    public class PlatformController : PersistentSingleton<PlatformController>
    {
        [SerializeField] private PlatformType _type;
        public PlatformType Type
        {
            get
            {
#if !UNITY_EDITOR && YG_PLUGIN_YANDEX_GAME
                _type = YandexGame.EnvironmentData.isMobile ? PlatformType.Mobile : PlatformType.PC;
#endif

                return _type;
            }
        }

        public void Set(PlatformType type) => _type = type;
    }
}