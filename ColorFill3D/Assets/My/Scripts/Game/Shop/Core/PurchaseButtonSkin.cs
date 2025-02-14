using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Project
{
    public class PurchaseButtonSkin : MonoBehaviour
    {
        [SerializeField] private ProductData _data;

        [SerializeField, Space(10)] private Image _lockImage;
        [SerializeField] private Button _buton;

        private int _index;

        private void OnEnable() => _buton.onClick.AddListener(Purchase);
        private void OnDisable() => _buton.onClick.RemoveListener(Purchase);

        private void Start()
        {
            _index = transform.GetSiblingIndex();
            _lockImage.gameObject.SetActive(!YandexGame.savesData.Skins[_index]);
        }

        private void Purchase()
        {
            if (!YandexGame.savesData.Skins[_index] && Shop.CheckBalance(YandexGame.savesData.Bank, _data.ParceSkin))
            {
                YandexGame.savesData.Skins[_index] = true;
                Shop.TakeBankValue(_data.ParceSkin);
            }

            if (YandexGame.savesData.Skins[_index])
            {
                YandexGame.savesData.CurrentTheme = _index;
                YandexGame.SaveProgress();
                EventBus.Instance.OnGetSkin?.Invoke(_index);
            }
        }
    }
}