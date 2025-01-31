using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = nameof(PixelArtItem), menuName = "Data/" + nameof(PixelArtItem))]
    public class PixelArtItem : ScriptableObject
    {
        public string Name => Texture.name;
        public Texture Texture;
    }
}