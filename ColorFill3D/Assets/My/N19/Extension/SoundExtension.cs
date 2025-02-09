using UnityEngine;

namespace N19
{
    public static class SoundExtension
    {
        public static void VolumeChange(this AudioSource source, float volume)
        {
            source.volume = volume;
        }

        public static bool SwitchStateMute(this AudioSource source)
        {
            return source.mute = !source.mute;
        }

        public static bool SetStateMute(this AudioSource source, bool isMute)
        {
            return source.mute = isMute;
        }
    }
}