using UnityEngine;

namespace Project.LevelBuilder
{
    public class GridGizmos : MonoBehaviour
    {
        [SerializeField] private Item _item;

        [SerializeField, Space(10)] private Vector3 _grid;
        [SerializeField] private Color32 _color = new(255, 255, 255, 255);

        [SerializeField, Space(10)] private bool _isDraw = true;

        private void OnDrawGizmos()
        {
            if (!_isDraw)
                return;

            Gizmos.color = _color;

            Vector3 scale = _item.gameObject.transform.localScale;
            Vector3 center = transform.position;

            Vector3 start = center - new Vector3(_grid.x * scale.x, _grid.y * scale.y, _grid.z * scale.z) * 0.5f;

            for (int y = 0; y <= _grid.y; y++)
            {
                for (int z = 0; z <= _grid.z; z++)
                {
                    Vector3 lineStart = start + new Vector3(0, y * scale.y, z * scale.z);
                    Vector3 lineEnd = lineStart + new Vector3(_grid.x * scale.x, 0, 0);
                    Gizmos.DrawLine(lineStart, lineEnd);
                }
            }

            for (int x = 0; x <= _grid.x; x++)
            {
                for (int z = 0; z <= _grid.z; z++)
                {
                    Vector3 lineStart = start + new Vector3(x * scale.x, 0, z * scale.z);
                    Vector3 lineEnd = lineStart + new Vector3(0, _grid.y * scale.y, 0);
                    Gizmos.DrawLine(lineStart, lineEnd);
                }
            }

            for (int x = 0; x <= _grid.x; x++)
            {
                for (int y = 0; y <= _grid.y; y++)
                {
                    Vector3 lineStart = start + new Vector3(x * scale.x, y * scale.y, 0);
                    Vector3 lineEnd = lineStart + new Vector3(0, 0, _grid.z * scale.z);
                    Gizmos.DrawLine(lineStart, lineEnd);
                }
            }
        }
    }
}