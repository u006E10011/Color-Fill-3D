using UnityEngine.UI;

namespace N19
{
    public static class UIExtension
    {
        /// <summary>
        /// Нормализует значение от 0 до 1
        /// </summary>
        public static float FillAmount(this Image image, float currentValue, float maxValue)
        {
            return image.fillAmount = UMath.Percent(currentValue, maxValue);
        }

        /// <summary>
        /// Нормализует значение от 0 до 1
        /// </summary>
        public static float FillAmount(this Slider slider, float currentValue, float maxValue)
        {
            return slider.value = UMath.Percent(currentValue, maxValue);
        }
    }
}