using UnityEngine;

namespace Project
{
    [System.Serializable]
    public class PlayerSoundController
    {
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
                N19.SoundControllerGlobal.Instance.PlaySFX(_startMove);
            }
        }


        public void PlayEndMoning()
        {
            if (!_isPlayeringEndMovingSound)
            {
                _isPlayeringEndMovingSound = true;
                N19.SoundControllerGlobal.Instance.PlaySFX(_endMoving);
            }
        }
    }
}