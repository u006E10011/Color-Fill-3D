using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Project
{
    public class SkinUI : MonoBehaviour
    {
        [SerializeField] private ProductData _data;

        [SerializeField, Space(10)] private Image _lockImage;
        [SerializeField] private Outline _outline;

        private int _index;

        private void OnValidate()
        {
            _index = transform.GetSiblingIndex();
            gameObject.name = _data.Skins[_index].name;
        }

        private void OnEnable() => EventBus.Instance.OnUpdateShopUI += UpdateUI;
        private void OnDisable() => EventBus.Instance.OnUpdateShopUI -= UpdateUI;

        private void Start() => UpdateUI();

        private void UpdateUI()
        {
            _lockImage.gameObject.SetActive(!YandexGame.savesData.Skins[_index]);
            _outline.enabled = YandexGame.savesData.CurrentSkin == _index;
        }
    }
}