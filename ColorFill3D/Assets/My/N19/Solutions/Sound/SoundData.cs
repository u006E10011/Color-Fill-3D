using System;

namespace N19
{
    public static class SoundData
    {
        public static Func<bool, bool> OnMuteSound;

        public static bool IsMute;

        public static float BackgroundSoundVolume = 1;
        public static float SFXSoundVolume = 1;

        public static bool BackgroundSoundEnable = true;
        public static bool SFXSoundEnable = true;
    }
}