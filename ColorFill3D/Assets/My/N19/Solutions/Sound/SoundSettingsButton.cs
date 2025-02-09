using UnityEngine;
using UnityEngine.UI;

namespace N19
{
    public class SoundSettingsButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        [SerializeField, Space(10)] private Sprite _on;
        [SerializeField] private Sprite _off;

        private void Reset()
        {
            _button = _button != null ? _button : GetComponent<Button>();
            _icon = _icon != null ? _icon : GetComponent<Image>();
        }

        private void Awake()
        {
            var isMute = SaveSystem.GetBool(Key.SOUND_ENABLED, false);
            _icon.sprite = isMute ? _off : _on;

            SoundData.OnMuteSound?.Invoke(SoundData.IsMute);
        }

        private void OnEnable() => _button.onClick.AddListener(Sound);
        private void OnDisable() => _button.onClick.RemoveListener(Sound);

        private void Sound()
        {
            var isMute = !SoundData.IsMute;
            SoundData.IsMute = isMute;
            _icon.sprite = isMute ? _off : _on;

            SoundData.OnMuteSound?.Invoke(isMute);
            SaveSystem.Save(Key.SOUND_ENABLED, isMute);
        }
    }
}