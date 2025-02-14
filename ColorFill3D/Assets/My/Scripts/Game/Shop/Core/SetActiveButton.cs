using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class SetActiveButton : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;
        [SerializeField] private Button _button;

        private void Reset()
        {
            _button = _button != null ? _button : GetComponent<Button>();
        }
        private void OnEnable() => _button.onClick.AddListener(SetActive);
        private void OnDisable() => _button.onClick.RemoveListener(SetActive);

        private void SetActive()
        {
            _menu.SetActive(!_menu.activeSelf);
        }
    }
}