using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [System.Serializable]
    public class SkinItemData
    {
        public int Parce;

        [Space(10)]
        public List<Sprite> Icon = new();
        public List<GameObject> Skins = new();

    }
}