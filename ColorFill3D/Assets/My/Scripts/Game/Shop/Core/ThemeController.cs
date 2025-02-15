using N19;
using UnityEngine;
using YG;

namespace Project
{
    public class ThemeController : MonoBehaviour
    {
        [SerializeField] private ProductData _data;

        public void TrySetTheme(int index)
        {
            if(index >= _data.Theme.Count)
            {
                Debug.Log("IndexOutOfRangeException ".Color(ColorType.Red) + index.Color(ColorType.Cyan));
                return;
            }

            if (_data.Theme[index].PassedLevelToUnlock <= YandexGame.savesData.LevelIndex)
            {
                YandexGame.savesData.Theme[index] = true;
                YandexGame.savesData.CurrentTheme = index;

                YandexGame.SaveProgress();
                EventBus.Instance.OnGetTheme?.Invoke(index);
                EventBus.Instance.OnUpdateShopUI?.Invoke();
            }
        }
    }
}