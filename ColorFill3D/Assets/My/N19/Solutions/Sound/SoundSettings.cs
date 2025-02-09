using UnityEngine;
using UnityEngine.UI;

namespace N19
{
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private Slider _sound;
        [SerializeField] private Slider SFX;

        private void Awake()
        {
            var background = SaveSystem.GetFloat(Key.SOUND_VOLUME, 1);
            var sfx = SaveSystem.GetFloat(Key.SFX_VOLUME, 1);

            BackgroundVolumeChange(background);
            SFXVolumeChange(sfx);
        }

        private void OnEnable()
        {
            _sound.onValueChanged.AddListener(BackgroundVolumeChange);
            SFX.onValueChanged.AddListener(SFXVolumeChange);
        }

        private void OnDisable()
        {
            _sound.onValueChanged.RemoveListener(BackgroundVolumeChange);
            SFX.onValueChanged.RemoveListener(SFXVolumeChange);
        }

        private void BackgroundVolumeChange(float volume)
        {
            _sound.value = volume;
            SoundData.BackgroundSoundVolume = volume;
            SaveSystem.Save(Key.SOUND_VOLUME, volume);
        }

        private void SFXVolumeChange(float volume)
        {
            SFX.value = volume;
            SoundData.SFXSoundVolume = volume;
            SaveSystem.Save(Key.SFX_VOLUME, volume);
        }
    }
}