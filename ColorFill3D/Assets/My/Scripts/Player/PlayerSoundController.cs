using UnityEngine;
using UnityEngine.Audio;

namespace Project
{
    [System.Serializable]
    public class PlayerSoundController
    {
        [SerializeField] private AudioMixerGroup _mixer;

        [SerializeField] private AudioClip _startMove;
        [SerializeField] private AudioClip _endMoving;

        private bool _isPlayeringEndMovingSound = true;
        private Vector3 _oldDirection;

        public void PlayStartMoving(Vector3 direction)
        {
            if (_oldDirection != direction)
            {
                _isPlayeringEndMovingSound = false;
                _oldDirection = direction;
                N19.SoundControllerGlobal.Instance.PlaySFX(_startMove).SetMixer(_mixer);
            }
        }


        public void PlayEndMoning()
        {
            if (!_isPlayeringEndMovingSound)
            {
                _isPlayeringEndMovingSound = true;
                N19.SoundControllerGlobal.Instance.PlaySFX(_endMoving).SetMixer(_mixer);
            }
        }
    }
}