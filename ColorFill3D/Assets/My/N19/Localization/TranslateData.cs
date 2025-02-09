using UnityEngine;

namespace N19
{
    [CreateAssetMenu(fileName = "Translate", menuName = "N19/Translate/" + nameof(TranslateData))]
    public class TranslateData : ScriptableObject
    {
        [TextArea] public string RU;
        [TextArea] public string EN;
        [TextArea] public string TR;

        [Tooltip("При нажатии на скрипт TranslateData, в инспекторе можно выбрать шрифт по умолчанию")]
        [Space(10)] public TMPro.TMP_FontAsset Font;

        [Space(10)]
        public float FontSizeRU;
        public float FontSizeEN;
        public float FontSizeTR;
    }
}