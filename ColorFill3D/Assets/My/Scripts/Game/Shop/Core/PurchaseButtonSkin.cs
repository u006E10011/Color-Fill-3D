using N19;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Project
{
    public class PurchaseButtonSkin : MonoBehaviour
    {
        [SerializeField] private ProductData _data;
        [SerializeField] private Button _buton;

        private void OnEnable() => _buton.onClick.AddListener(Purchase);
        private void OnDisable() => _buton.onClick.RemoveListener(Purchase);

        private void Purchase()
        {
            var index = transform.GetSiblingIndex();

            if(index >= _data.Skins.Count)
            {
                Debug.Log("IndexOutOfRangeException ".Color(ColorType.Red) + index.Color(ColorType.Cyan));
                return;
            }

            if (!YandexGame.savesData.Skins[index] && Shop.CheckBalance(YandexGame.savesData.Bank, _data.ParceSkin))
            {
                YandexGame.savesData.Skins[index] = true;
                Shop.TakeBankValue(_data.ParceSkin);
            }

            if (YandexGame.savesData.Skins[index])
            {
                YandexGame.savesData.CurrentSkin = index;
                YandexGame.SaveProgress();
                EventBus.Instance.OnGetSkin?.Invoke(index);
                EventBus.Instance.OnUpdateShopUI?.Invoke();
            }
        }
    }
}