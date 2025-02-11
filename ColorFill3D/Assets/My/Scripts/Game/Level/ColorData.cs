using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Project
{
    [CreateAssetMenu(fileName = nameof(ColorData), menuName = "Data/" + nameof(ColorData))]
    public class ColorData : ScriptableObject
    {
        [Space(10)] public Color ColortPlayerOffset;

        [Space(10)]
        public Color ColortCell = new(1, 1, 1, 1);
        public Color ColorCubeMap = new(1, 1, 1, 1);

        [Space(10)]public List<Color> ColorBrush = new();

        public Color GetColor()
        {
            var index = Mathf.Clamp((int)(YandexGame.savesData.LevelIndex / 10f), 0, ColorBrush.Count - 1);
            return ColorBrush[index];
        }
    }
}