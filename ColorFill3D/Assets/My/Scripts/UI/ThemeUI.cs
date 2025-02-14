using N19;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Project
{
    public class ThemeUI : MonoBehaviour
    {
        [SerializeField] private ProductData _data;
        [SerializeField] private ThemeController _theme;

        [SerializeField, Space(10)] private Outline _outline;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        [SerializeField, Space(5)] private Translate _unlockInfo;

        private int _index;

        private void OnValidate()
        {
            _index = transform.GetSiblingIndex();

            gameObject.name = _data.Theme[_index].Name;
            _icon.sprite = _data.Theme[_index].Icon;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Select);
            EventBus.Instance.OnUpdateShopUI += UpdateUI;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Select);
            EventBus.Instance.OnUpdateShopUI -= UpdateUI;
        }

        private void Start() => UpdateUI();

        private void Select()
        {
            _theme.TrySetTheme(_index);
        }

        private void UpdateUI()
        {
            _outline.enabled = YandexGame.savesData.CurrentTheme == _index;
            _unlockInfo.gameObject.SetActive(!YandexGame.savesData.Theme[_index]);
            _unlockInfo.Replace(_data.Theme[_index].PassedLevelToUnlock);
        }
    }
}