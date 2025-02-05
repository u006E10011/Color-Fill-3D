using UnityEngine;

namespace Project
{
    public class Cell : LevelBuilder.Item
    {
        [SerializeField] private Color32 _color = new(255, 255, 255, 255);

        [SerializeField] private SpriteRenderer _renderer;

        private void OnEnable()
        {
            Brush.Add(gameObject, this);
        }

        private void OnDisable()
        {
            Brush.Remove(gameObject);
        }

        public void SetColor()
        {
            _renderer.color = _color;
        }
    }
}