#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Project.LevelBuilder
{
    public class DrawGizmos : MonoBehaviour
    {
        public LevelBuilder LevelBuilder;
        [Space(10)] public Vector3 Grid = new(10, 1, 10);

        [Header("Color")]
        public Color32 ColorGrid = Color.blue;

        [Space(5)]
        public Color32 ColorEmptyCell = Color.green;
        public Color32 ColorFullCell = Color.red;

        [Space(10)] public bool IsDraw = true;

        private Item _item;

        private void OnDrawGizmos()
        {
            DrawGrid();
            DrawCube();
        }

        private void DrawGrid()
        {
            if (!IsDraw || Application.isPlaying)
                return;

            Gizmos.color = ColorGrid;

            Vector3 scale = Vector3.one;
            Vector3 center = transform.position + new Vector3(.5f, 0, .5f);
            Vector3 start = center - new Vector3(Grid.x * scale.x, Grid.y * scale.y, Grid.z * scale.z) * 0.5f;

            for (int y = 0; y <= Grid.y; y++)
            {
                for (int z = 0; z <= Grid.z; z++)
                {
                    Vector3 lineStart = start + new Vector3(0, y * scale.y, z * scale.z);
                    Vector3 lineEnd = lineStart + new Vector3(Grid.x * scale.x, 0, 0);
                    Gizmos.DrawLine(lineStart, lineEnd);
                }
            }

            for (int x = 0; x <= Grid.x; x++)
            {
                for (int z = 0; z <= Grid.z; z++)
                {
                    Vector3 lineStart = start + new Vector3(x * scale.x, 0, z * scale.z);
                    Vector3 lineEnd = lineStart + new Vector3(0, Grid.y * scale.y, 0);
                    Gizmos.DrawLine(lineStart, lineEnd);
                }
            }

            for (int x = 0; x <= Grid.x; x++)
            {
                for (int y = 0; y <= Grid.y; y++)
                {
                    Vector3 lineStart = start + new Vector3(x * scale.x, y * scale.y, 0);
                    Vector3 lineEnd = lineStart + new Vector3(0, 0, Grid.z * scale.z);
                    Gizmos.DrawLine(lineStart, lineEnd);
                }
            }
        }

        #region Raycast
        private void DrawCube()
        {
            var scale = Vector3.one;

            if (GetPoint(out var point))
            {
                if (!Exist(_item))
                {
                    Gizmos.color = ColorEmptyCell;
                    Gizmos.DrawCube(point, scale);
                }
                else
                {
                    Gizmos.color = ColorFullCell;
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

    }
}
#endif