using System;
using System.Collections.Generic;

namespace N19
{
    public static class TimeSuffixDesignation
    {
        private readonly static List<string> TIME_SUFFIX_RU = new(4) { "мс", "с", "мин", "ч" };
        private readonly static List<string> TIME_SUFFIX_EN = new(4) { "ms", "s", "min", "h" };
        private readonly static List<string> TIME_SUFFIX_TR = new(4) { "ms", "sn", "dk", "sa" };

        private static string _suffix;
        private static TimeSpan _span;

        public static string GetSuffix(float time)
        {
            _span = TimeSpan.FromSeconds(time);

            if (_span.Hours > 0)
                _suffix = SuffixData(3);
            else if (_span.Minutes > 0)
                _suffix = SuffixData(2);
            else if (_span.Seconds > 0)
                _suffix = SuffixData(1);
            else if (_span.Milliseconds >= 0)
                _suffix = SuffixData(0);

            return _suffix;
        }

        private static string SuffixData(int index)
        {
            return Language.Value switch
            {
                LanguageType.RU => TIME_SUFFIX_RU[index],
                LanguageType.EN => TIME_SUFFIX_EN[index],
                LanguageType.TR => TIME_SUFFIX_TR[index],
                _ => string.Empty
            };
        }
    }
}