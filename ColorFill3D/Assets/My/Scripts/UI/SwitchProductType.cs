using System.Collections.Generic;
using N19;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Project
{
    public class SwitchProductType : MonoBehaviour
    {
        [SerializeField] private Color32 _selectedColor;
        [SerializeField] private Color32 _defoltColor;

        [SerializeField] private List<Image> _content = new();
        [SerializeField] private List<Button> _button = new();
        [SerializeField] private List<TMP_Text> _text = new();

        private void OnEnable()
        {
            for (int i = 0; i < _button.Count; i++)
                _button[i].onClick.AddListener(Select(i));
        }

        private void OnDisable()
        {
            for (int i = 0; i < _button.Count; i++)
                _button[i].onClick.RemoveListener(Select(i));
        }

        private void Start() => Select(0)();

        private UnityAction Select(int index) => () =>
        {
            for (int i = 0; i < _button.Count; i++)
            {
                _text[i].color = i == index ? _selectedColor : _defoltColor;
                _content[i].gameObject.SetActive(i == index);
            }
        };
    }
}