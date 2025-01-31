using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class PixelArtGenerator : MonoBehaviour
    {
        [SerializeField] private Texture2D _inputTexture;
        [SerializeField] private SpriteRenderer _pixelPrefab;

        [SerializeField] private Vector3 _rotation;

        private static Dictionary<Vector3Int, SpriteRenderer> _pixel = new();
        private Transform _container;
        private Vector3Int _offset;

        void Awake()
        {
            Init();
            GenerateTexture();
            OffsetTexture();
        }

        private void Init()
        {
            _pixel = new();
            _container = new GameObject("PixelArt").transform;
            _offset = new Vector3Int(_inputTexture.width / 2, _inputTexture.height / 2, 0);
        }

        public Texture2D GetTexture() => _inputTexture;

        private void GenerateTexture()
        {
            if (_inputTexture == null || _pixelPrefab == null)
            {
                Debug.LogError("Input texture or pixel prefab is not assigned!");
                return;
            }

            int width = _inputTexture.width;
            int height = _inputTexture.height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var position = Vector3Int.CeilToInt(new Vector3Int(x, y, 0));
                    var pixel = Instantiate(_pixelPrefab, position, Quaternion.identity, _container);

                    _pixel.Add(new(position.x, 0, position.y), pixel);
                }
            }
        }

        private void OffsetTexture()
        {
            _container.position -= new Vector3(_offset.x, 0, _offset.y);
            _container.rotation = Quaternion.Euler(_rotation);
        }

        public static bool TryGetValue(Vector3Int point, out SpriteRenderer pixel)
        {
            return _pixel.TryGetValue(point, out pixel);
        }
    }
}