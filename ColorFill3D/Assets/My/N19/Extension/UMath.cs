namespace N19
{
    public static class UMath
    {
        /// <summary>
        /// Нормализует значение от 0 до 1
        /// </summary>
        public static float Percent(float currentValue, float maxValue)
        {
            return currentValue / maxValue;
        }
    }
}