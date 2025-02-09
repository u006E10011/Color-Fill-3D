using UnityEngine;
using UnityEngine.UI;

namespace N19
{
    public class SoundButtonClick : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private AudioClip _audioClip;

        private void Reset() => _button = _button != null ? _button : GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(Play);
        private void OnDisable() => _button.onClick.RemoveListener(Play);

        private void Play() => SoundControllerGlobal.Instance.PlaySFX(_audioClip);
    }
}