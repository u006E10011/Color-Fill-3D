using System;
using UnityEngine;

namespace Project
{
    public class Cube : LevelBuilder.Item
    {
        [SerializeField] private ProductData _data;
        [SerializeField] private MeshRenderer _renderer;

        private void OnEnable() => EventBus.Instance.OnGetTheme += Get;
        private void OnDisable() => EventBus.Instance.OnGetTheme -= Get;

        private void Get(int index)
        {
            _renderer.material = _data.Theme[index].MaterialCube;
        }
    }
}