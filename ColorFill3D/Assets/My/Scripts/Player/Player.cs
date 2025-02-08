using UnityEngine;

namespace Project
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ColorData _data;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _offset;

        private void OnEnable() => EventBus.Instance.OnUpdateProgress += ChangeColor;
        private void OnDisable() => EventBus.Instance.OnUpdateProgress -= ChangeColor;

        private void ChangeColor()
        {
            _renderer.material.color = _data.GetColor() - _data.ColortPlayerOffset;
        }
    }
}