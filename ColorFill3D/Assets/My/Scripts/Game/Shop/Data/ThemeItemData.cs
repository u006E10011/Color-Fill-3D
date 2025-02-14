using UnityEngine;

namespace Project
{
    [System.Serializable]
    public struct ThemeItemData
    {
        public string Name;
        public int PassedLevelToUnlock;

        [Space(10)]
        public Material MaterialCube;
        public Material MaterialCell;
    }
}