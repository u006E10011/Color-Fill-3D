namespace N19
{
    public enum LanguageType
    {
        RU,
        EN,
        TR
    }

    public static class Language
    {
        public static event System.Func<Translate> OnSetTranslate;
        public static event System.Action OnNowTranslate;

        public static LanguageType Value = LanguageType.RU;

        public static void Check()
        {
            Value = YG.YandexGame.EnvironmentData.language switch
            {
                "ru" => LanguageType.RU,
                "en" => LanguageType.EN,
                "tr" => LanguageType.TR,
                _ => LanguageType.EN
            };
        }

        public static void Select(LanguageType type)
        {
            Value = type;

            OnSetTranslate?.Invoke();
            OnNowTranslate?.Invoke();
        }
    }
}