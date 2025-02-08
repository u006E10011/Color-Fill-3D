﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        [SerializeField] private ColorData _data;
        [SerializeField] private TMP_Text _levelText;

        private float _count;

        private void OnEnable()
        {
            EventBus.Instance.OnUpdateProgress += UpdateProgress;
            EventBus.Instance.OnCompleteLevel += UpdateData;
        }

        private void OnDisable()
        {
            EventBus.Instance.OnUpdateProgress -= UpdateProgress;
            EventBus.Instance.OnCompleteLevel -= UpdateData;
        }

        private void Start()
        {
            UpdateData();
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            _fill.fillAmount = 1f - (Brush.Count() / _count);
            _levelText.text = Brush.Count().ToString();
        }

        private void UpdateData()
        {
            _fill.color = _data.GetColor();
            _count = Brush.Count();
            _fill.fillAmount = 1f - (Brush.Count() / _count);
        }
    }
}