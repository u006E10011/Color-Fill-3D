using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace N19
{
    [CreateAssetMenu(fileName = nameof(FootSoundData), menuName = "N19/PlayerController/" + nameof(FootSoundData))]
    public class FootSoundData : ScriptableObject
    {
        public float Volume = 1;
        public AudioMixerGroup AudioMixer;

        [Space(10)]
        public MinMax IntervalWalk = new(0.1f, 0.2f);
        public MinMax IntervalRunning = new(0.05f, 0.1f);
        public MinMax Pich = new(0.95f, 1.05f);

        public AudioClip JumpUpClip;
        public AudioClip JumpDownClip;

        [Space(5)]public List<AudioClip> MoveClip = new();
    }
}