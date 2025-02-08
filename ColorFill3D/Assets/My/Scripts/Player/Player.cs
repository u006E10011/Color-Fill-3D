using UnityEngine;

namespace Project
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ColorData _data;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _offset;

        private void Start()
        {
            _renderer.material.color = _data.GetColor() - _data.ColortPlayerOffset;
        }
    }
}