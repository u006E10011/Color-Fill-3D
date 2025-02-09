using UnityEngine;

namespace N19
{
    public class FootSound : MonoBehaviour
    {
        [SerializeField] private FootSoundData _data;

        private SoundControllerInstance _source;
        private float _time;

        private void Awake()
        {
            _source = gameObject.AddComponent<SoundControllerInstance>();
            _source.SetMixer(_data.AudioMixer).SetVolume(_data.Volume);
        }

        private void OnEnable()
        {
            PlayerControllerEvent.OnRunning += Run;
            PlayerControllerEvent.OnWalk += Walk;
            PlayerControllerEvent.OnJumpUp += JumpUp;
            PlayerControllerEvent.OnJumpDown += JumpDown;
        }

        private void OnDisable()
        {
            PlayerControllerEvent.OnRunning -= Run;
            PlayerControllerEvent.OnWalk -= Walk;
            PlayerControllerEvent.OnJumpUp -= JumpUp;
            PlayerControllerEvent.OnJumpDown -= JumpDown;
        }

        private void Update() => _time += Time.deltaTime;

        private void Play(MinMax interval)
        {
            if (_data.MoveClip.Count == 0)
            {
                Debug.Log("Food sound count 0".Color(ColorType.Cyan));
                return;
            }

            if (_time >= Random.Range(interval.Min, interval.Max))
            {
                var index = Random.Range(0, _data.MoveClip.Count);
                var clip = _data.MoveClip[index];

                _source.PlaySFX(clip).SetPitch(Random.Range(_data.Pich.Min, _data.Pich.Max));

                _time = 0;
            }
        }

        private void Walk() => Play(_data.IntervalWalk);
        private void Run() => Play(_data.IntervalRunning);
        private void JumpUp() => _source.PlaySFX(_data.JumpUpClip);
        private void JumpDown() => _source.PlaySFX(_data.JumpDownClip);
    }
}