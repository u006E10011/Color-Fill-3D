using UnityEngine;

using static N19.Language;

namespace N19
{
    public class GetTypeLanguage : MonoBehaviour
    {
        [SerializeField] private LanguageType language;

#if UNITY_EDITOR
        private void OnValidate() => Select(language);

#elif YG_PLUGIN_YANDEX_GAME
        private void OnEnable() => YG.YandexGame.GetDataEvent += GetData;
        private void OnDisable() => YG.YandexGame.GetDataEvent -= GetData;
#endif

        private void Start() => GetData();

        private void GetData()
        {
#if !UNITY_EDITOR
            Check();
#endif
            language = Value;
        }
    }
}