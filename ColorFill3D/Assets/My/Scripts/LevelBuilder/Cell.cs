using UnityEngine;

namespace Project
{
    public class Cell : Item
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                Brush.Paint(gameObject);
                GameController.OnUpdateProgress?.Invoke();
            }
        }
    }
}