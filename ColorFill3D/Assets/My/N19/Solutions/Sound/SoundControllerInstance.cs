using UnityEngine;
using UnityEngine.Audio;

namespace N19
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundControllerInstance : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        public SoundType Type { get; private set; } = SoundType.SFX;

        private void Reset() => _audioSource = _audioSource != null ? _audioSource : GetComponent<AudioSource>();
        public void Awake() => _audioSource = _audioSource != null ? _audioSource : GetComponent<AudioSource>();

        private void Start() => SetStateMute(SoundData.IsMute);

        private void OnEnable() => SoundData.OnMuteSound += SetStateMute;
        private void OnDisable() => SoundData.OnMuteSound -= SetStateMute;

        public SoundControllerInstance SetType(SoundType type)
        {
            Type = type;

            return this;
        }

        public SoundControllerInstance PlaySFX(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.Log("<color=white>SFX</color> audio clip <color=red>is null</color>");
                return this;
            }

            _audioSource.volume = SoundData.SFXSoundVolume;
            _audioSource.PlayOneShot(clip);

            return this;
        }

        public SoundControllerInstance PlayMainSound()
        {
            if (_audioSource.clip == null)
            {
                Debug.Log("<color=white>Main Sound</color> audio clip <color=red>is null</color>");
                return this;
            }

            _audioSource.volume = SoundData.BackgroundSoundVolume;
            _audioSource.Play();

            return this;
        }

        #region Extension
        public SoundControllerInstance SetVolume(float volume)
        {
            _audioSource.volume = volume;

            return this;
        }

        public SoundControllerInstance SetPitch(float pitch)
        {
            _audioSource.pitch = pitch;

            return this;
        }

        public SoundControllerInstance SetMixer(AudioMixerGroup mixer)
        {
            _audioSource.outputAudioMixerGroup = mixer;

            return this;
        }

        public SoundControllerInstance SetMainSound(AudioClip clip, bool isPlaing = true)
        {
            _audioSource.clip = clip;

            if (isPlaing)
                _audioSource.Play();

            return this;
        }

        public SoundControllerInstance SetPosition(Vector3 position)
        {
            gameObject.transform.position = position;

            return this;
        }

        public SoundControllerInstance SetLoop(bool isLoop)
        {
           _audioSource.loop = isLoop;

            return this;
        }
        #endregion

        #region Mute
        public bool SwitchState()
        {
            var isMute = _audioSource.SwitchStateMute();
            SoundData.IsMute = isMute;
            return isMute;
        }

        public bool SetStateMute(bool isMute)
        {
            SoundData.IsMute = isMute;
            return _audioSource.SetStateMute(isMute);
        }
        #endregion
    }
}