using UnityEngine;

namespace Project
{
    public class Cell : LevelBuilder.Item
    {
        [SerializeField] private ColorData _data;

        [SerializeField] private SpriteRenderer _renderer;

        private void OnEnable() => Brush.Add(gameObject, this);
        private void OnDisable() => Brush.Remove(gameObject);

        private void Start() => _renderer.color = _data.ColortCell;

        public void SetColor()
        {
            _renderer.color = _data.GetColor();
        }
    }
}