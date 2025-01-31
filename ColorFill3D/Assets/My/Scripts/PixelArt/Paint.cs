using UnityEngine;

namespace Project
{
    public class Paint : MonoBehaviour
    {
        [SerializeField] private PixelArtGenerator _generator;

        private void OnTriggerEnter(Collider other)
        {
            var point = Vector3Int.CeilToInt(other.transform.localPosition);
            Draw(new(point.x, 0, point.y));
        }

        public void Draw(Vector3Int point)
        {
            if(PixelArtGenerator.TryGetValue(point, out var pixel))
            {
                pixel.color = _generator.GetTexture().GetPixel(point.x, point.z);
                Destroy(pixel.GetComponent<Collider>());
            }
        }
    }
}