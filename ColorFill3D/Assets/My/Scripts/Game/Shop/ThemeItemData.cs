using UnityEngine;

namespace Project
{
    [System.Serializable]
    public struct ThemeItemData
    {
        public string Name;

        [Space(10)]
        public Material Material;
        public Color Color;
    }
}