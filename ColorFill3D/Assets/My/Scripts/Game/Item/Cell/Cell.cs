using N19;
using UnityEngine;

namespace Project
{
    public class Cell : LevelBuilder.Item
    {
        [SerializeField] private ColorData _colorData;
        [SerializeField] private ProductData _productData;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private MeshRenderer _meshRenderer;

        private void OnEnable()
        {
            Brush.Add(gameObject, this);
            EventBus.Instance.OnGetTheme += Get;
        }

        private void OnDisable()
        {
            Brush.Remove(gameObject);
            EventBus.Instance.OnGetTheme -= Get;
        }

        public void SetColor()
        {
            _meshRenderer.gameObject.Deactivate();
            _spriteRenderer.gameObject.Activate();
            _spriteRenderer.color = _colorData.GetColor();
        }

        private void Get(int index)
        {
            _meshRenderer.material = _productData.Theme[index].MaterialCell;
        }
    }
}