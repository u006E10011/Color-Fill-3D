using N19;
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
            if (index >= _data.Theme.Count)
            {
                Debug.Log("IndexOutOfRangeException " + index.Color(ColorType.Cyan));
                return;
            }

            _renderer.material = _data.Theme[index].MaterialCube;
        }
    }
}