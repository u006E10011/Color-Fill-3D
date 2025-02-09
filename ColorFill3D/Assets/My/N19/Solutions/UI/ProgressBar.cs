using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace N19
{
    public static class ProgressBar
    {
        #region ProgressBarImage
        /// <summary>
        /// Нормализует значение от 0 до 1
        /// </summary>
        public static void ValueChange(Image image, float currentValue, float maxValue)
        {
            UIExtension.FillAmount(image, currentValue, maxValue);
        }

        /// <summary>
        /// Нормализует значение от 0 до 1, выводит в текст без нормализации
        /// </summary>
        public static void ValueChange(Image image, TMP_Text text, float currentValue, float maxValue, float minValue = 0, bool visibleMaxValue = false)
        {
            UIExtension.FillAmount(image, currentValue, maxValue);
            text.text = visibleMaxValue
                ? $"{Mathf.Clamp(currentValue, minValue, maxValue)} / {maxValue}"
                : Mathf.Clamp(currentValue, minValue, maxValue).ToString();
        }

        #endregion

        #region ProgressBarSlider
        /// <summary>
        /// Нормализует значение от 0 до 1
        /// </summary>
        public static void ValueChange(Slider slider, float currentValue, float maxValue)
        {
            UIExtension.FillAmount(slider, currentValue, maxValue);
        }

        /// <summary>
        /// Нормализует значение от 0 до 1, выводит в текст без нормализации
        /// </summary>
        public static void ValueChange(Slider slider, TMP_Text text, float currentValue, float maxValue, float minValue = 0)
        {
            UIExtension.FillAmount(slider, currentValue, maxValue);
            text.text = Mathf.Clamp(currentValue, minValue, maxValue).ToString();
        }
        #endregion
    }
}