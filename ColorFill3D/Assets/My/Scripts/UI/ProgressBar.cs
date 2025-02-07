using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        [SerializeField] private TMP_Text _levelText;

        private float _count;

        private void OnEnable() => EventBus.Instance.OnUpdateProgress += UpdateProgress;
        private void OnDisable() => EventBus.Instance.OnUpdateProgress -= UpdateProgress;

        private void Start()
        {
            _count = Brush.Count();
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            _fill.fillAmount = 1f - (Brush.Count() / _count);
            _levelText.text = Brush.Count().ToString();
        }
    }
}