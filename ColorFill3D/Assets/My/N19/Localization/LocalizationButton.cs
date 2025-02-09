using N19;
using UnityEngine;
using UnityEngine.UI;

namespace N19
{
    public class LocalizationButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;

        [SerializeField, Space(10)] private Sprite _ru;
        [SerializeField] private Sprite _en;

        private void Reset() => _button = _button != null ? _button : GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(Switch);
        private void OnDisable() => _button.onClick.RemoveListener(Switch);

        private void Start() => _icon.sprite = Language.Value == LanguageType.RU ? _ru : _en;

        private void Switch()
        {
            if (_icon.sprite == _ru)
            {
                Language.Select(LanguageType.EN);
                _icon.sprite = _en;
            }
            else
            {
                Language.Select(LanguageType.RU);
                _icon.sprite = _ru;
            };
        }
    }
}