using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = nameof(ProductData), menuName = "Data/" + nameof(ProductData))]
    public class ProductData : ScriptableObject
    {
        public int ParceSkin;

        [Space(10)]
        public List<GameObject> Skins = new();
        public List<ThemeItemData> Theme = new();
    }
}