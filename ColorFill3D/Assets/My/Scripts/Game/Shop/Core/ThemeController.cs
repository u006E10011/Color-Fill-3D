using UnityEngine;
using YG;

namespace Project
{
    public class ThemeController : MonoBehaviour
    {
        [SerializeField] private ProductData _data;

        public void TrySetTheme(int index)
        {
            if (_data.Theme[index].PassedLevelToUnlock < YandexGame.savesData.LevelIndex)
            {
                YandexGame.savesData.Theme[index] = true;
                YandexGame.savesData.CurrentTheme = index;

                YandexGame.SaveProgress();
                EventBus.Instance.OnGetTheme?.Invoke(index);
                EventBus.Instance.OnUpdateThemeUI?.Invoke();
            }
        }
    }
}