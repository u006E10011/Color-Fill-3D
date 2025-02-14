using TMPro;
using UnityEngine;

namespace Project
{
    public class ViewBankValue : MonoBehaviour
    {
        [SerializeField] private TMP_Text _valueText;

        private void OnEnable() => EventBus.Instance.OnUpdateShopUI += UpdateUI;
        private void OnDisable() => EventBus.Instance.OnUpdateShopUI -= UpdateUI;

        private void UpdateUI()
        {
            _valueText.text = YG.YandexGame.savesData.Bank.ToString();
        }
    }
}