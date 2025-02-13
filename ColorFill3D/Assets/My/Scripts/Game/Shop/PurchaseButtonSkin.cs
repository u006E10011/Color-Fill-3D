using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Project
{
    public class PurchaseButtonSkin : MonoBehaviour
    {
        [SerializeField] private ProductData _data;
        [SerializeField] private int _index;

        [SerializeField, Space(10)] private Button _buton;

        private void OnEnable() => _buton.onClick.AddListener(Purchase);
        private void OnDisable() => _buton.onClick.RemoveListener(Purchase);

        private void Purchase()
        {
            if (!YandexGame.savesData.Skins[_index] && Shop.CheckBalance(YandexGame.savesData.Bank, _data.ParceSKin))
            {
                YandexGame.savesData.Skins[_index] = true;
                Shop.TakeBankValue(_data.ParceSKin);
            }

            if (YandexGame.savesData.Skins[_index])
            {
                YandexGame.savesData.CurrentThemeSkin = _index;
                EventBus.Instance.OnGetSkin?.Invoke(_index);
            }
        }
    }
}