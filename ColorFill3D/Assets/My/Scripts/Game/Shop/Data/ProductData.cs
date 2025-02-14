using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = nameof(ProductData), menuName = "Data/" + nameof(ProductData))]
    public class ProductData : ScriptableObject
    {
        public SkinItemData Skin = new();
        public List<ThemeItemData> Theme = new();
    }
}