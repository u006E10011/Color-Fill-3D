using TMPro;
using UnityEngine;

using static N19.Language;

namespace N19
{
    public class Translate : MonoBehaviour
    {
        private const string REPLACE_SYMBOL = "&";

        [SerializeField] private bool _translateOnAwake = true;
        [SerializeField] private bool _translateOnEnable = true;

        [SerializeField, Space(10)] private bool _isCastomFontSize;

        [Space(10)] public TMP_Text Text;

        [SerializeField, Space(10)] private TranslateData _data;

        public string Value
        {
            get => _value;
            private set => _value = value;
        }

        private string _value = string.Empty;
        private float _fontSize;

        private void Reset() => Text = Text != null ? Text : GetComponent<TMP_Text>();
        private void OnValidate() => Text.font = _data?.Font;

        private void Awake()
        {
            SetFont();

            if (_translateOnAwake)
                Set();

            if (_isCastomFontSize)
                Text.fontSize = _fontSize;

            OnSetTranslate += Set;
        }

        private void OnEnable()
        {
            if (_translateOnEnable)
                Set();
        }

        private void OnDestroy() => OnSetTranslate -= Set;

        #region Base
        /// <summary>
        /// Вставляет в поле с текстом текст, который находится в конфиге, в зависимости он выбранного языка. Не советуется использовать в методе Update
        /// </summary>
        public Translate Set()
        {
            GetValue();

            Text.text = _value;

            if (_isCastomFontSize)
                Text.fontSize = _fontSize;

            return this;
        }

        /// <summary>
        ///  Задаёт текст
        /// </summary>
        public Translate Set(string value)
        {
            Text.text = value;

            return this;
        }

        /// <summary>
        /// Добавляет к текущему тексту ваш текст
        /// 
        /// <br> </br>
        /// <br>Примечание</br>
        /// <br>Если метод Set ниразу не вызывался, то он вызывится</br>
        /// </summary>
        /// <param name="value">Ваш текст</param>
        /// <returns>Возвращает отформатированный текст</returns>
        public string Add(object value)
        {
            if (_value == string.Empty)
                Set();

            Text.text += value;

            return Text.text;
        }
        #endregion

        #region Replase
        /// <summary>
        /// Заменяет специльаный симвов, на переданный текст
        /// </summary>
        /// <param name="value">Текст</param>
        /// <param name="path">Специальный символ</param>
        /// <returns></returns>
        public Translate Replace(object value, string path = REPLACE_SYMBOL)
        {
            if (_value == string.Empty)
                Set();

            Text.text = _value.Replace(path, value.ToString());

            return this;
        }

        /// <summary>
        /// Заменяет специльаный символ, на переданный список текстов. Замена происходит в хронологическом порядке
        /// </summary>
        /// <param name="value">Список текстов</param>
        /// <param name="path">Специальный символ</param>
        /// <returns></returns>
        //public string Replace(string path = REPLACE_SYMBOL, params object[] value)
        //{
        //    if (_value == string.Empty)
        //        Set();

        //    for (int i = 0; i < value.Length; i++)
        //        _value.Replace(path, value[i].ToString());

        //    return _text.text = _value;
        //}
        #endregion

        #region Data
        /// <summary>
        /// Задаёт конфиг
        /// </summary>
        /// <param name="config">Конфиг с переводом</param>
        public void SetData(TranslateData config) => _data = config;
        private void GetValue()
        {
            DefaudData();

            _value = Language.Value switch
            {
                LanguageType.RU => _data.RU,
                LanguageType.EN => _data.EN,
                LanguageType.TR => _data.TR,
                _ => _data.EN
            };

            _fontSize = Language.Value switch
            {
                LanguageType.RU => _data.FontSizeRU,
                LanguageType.EN => _data.FontSizeEN,
                LanguageType.TR => _data.FontSizeTR,
                _ => _data.FontSizeEN
            };

            SetFont();
        }

        private void DefaudData()
        {
            _data ??= new()
            {
                RU = "Data is null".Color(ColorType.Red),
                EN = "Data is null".Color(ColorType.Red),
                TR = "Data is null".Color(ColorType.Red)
            };
        }

        private void SetFont()
        {
            if (_data && Text.font && (Text.font != _data.Font))
                Text.font = _data.Font;
        }
        #endregion
    }
}