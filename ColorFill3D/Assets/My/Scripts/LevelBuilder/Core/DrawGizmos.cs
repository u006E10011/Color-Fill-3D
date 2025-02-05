using UnityEditor;
using UnityEngine;

namespace Project.LevelBuilder
{
    public class DrawGizmos : MonoBehaviour
    {
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private Player _player;
        [SerializeField, Space(10)] private Vector3 _grid;

        [Header("Color")]
        [SerializeField] private Color32 _colorGrid = Color.blue;
        [SerializeField] private Color32 _colorPlayer = Color.cyan;
        [SerializeField, Space(5)] private Color32 _colorEmptyCell = Color.green;
        [SerializeField] private Color32 _colorFillCell = Color.red;

        [SerializeField, Space(10)] private bool _isDraw = true;

        private Item _item;

        private void OnDrawGizmos()
        {
            DrawGrid();
            DrawCube();
            DrawPlayer();
        }

        private void DrawGrid()
        {
            if (!_isDraw || Application.isPlaying)
                return;

            Gizmos.color = _colorGrid;

            Vector3 scale = _player.transform.localScale;
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

        #region Raycast
        private void DrawCube()
        {
            var scale = _player.transform.localScale;

            if (GetPoint(out var point))
            {
                if (!Exist(_item))
                {
                    Gizmos.color = _colorEmptyCell;
                    Gizmos.DrawCube(point, scale);
                }
                else
                {
                    Gizmos.color = _colorFillCell;
                    Gizmos.DrawCube(point, scale);
                }
            }
        }

        public bool GetPoint(out Vector3 position)
        {
            var result = Physics.Raycast(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition), out var hitInfo, Mathf.Infinity);

            position = new(Mathf.Round(hitInfo.point.x), 0, Mathf.Round(hitInfo.point.z));

            if (result && hitInfo.collider != null)
                _item = hitInfo.collider.GetComponent<Item>();

            return result;
        }

        public bool Exist(Item item)
        {
            return item;
        }
        #endregion

        private void DrawPlayer()
        {
            Gizmos.color = _colorPlayer;
            Gizmos.DrawCube(_player.transform.position, _player.transform.localScale);

        }
    }
}