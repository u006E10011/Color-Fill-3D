using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

using static Project.LevelBuilder.LevelBuilderInput;

namespace Project.LevelBuilder
{
    public class LevelBuilderWindow : EditorWindow
    {
        private int _selectedAction = default;
        private string[] _labels = new string[5];

        private Vector2 LabelScale => new(position.width - 5, 30);

        private LevelBuilder _levelBuilder;
        private LevelBuilderData _data;
        private DrawGizmos _gizmo;
        private GameObject _gizmoContainer;

        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;

            _data = AssetDatabase.LoadAssetAtPath(@"Assets/My/Data/LevelBuilderData.asset", typeof(LevelBuilderData)) as LevelBuilderData;
            _levelBuilder = new();
            _labels = _data.Items.Select(p => p.name).ToArray();

            CreateGizmos();
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        [MenuItem("Tool/LevelBuilder")]
        private static void Init()
        {
            LevelBuilderWindow window = (LevelBuilderWindow)GetWindow(typeof(LevelBuilderWindow));
            window.Show();
        }

        private void OnGUI()
        {
            GizmosInput();
            EditorButton();
            SavePrefab();
        }

        private void OnSceneGUI(SceneView sceneview)
        {
            var e = Event.current;
            Update(e);

            if (e.type == EventType.Repaint)
                return;

            var mouseLeft = IsMouseLeftPressed || IsMouseLeftClick;
            var mouseRight = IsMouseRightPressed || IsMouseRightClick;

            var add = IsCTRLPressed && mouseLeft;
            var remove = IsCTRLPressed && mouseRight;

            var validateIndex = _selectedAction >= 0 && _selectedAction < _data.Items.Count;

            if (remove)
            {
                _levelBuilder.Remove();
                e.Use();
            }
            else if (add && validateIndex)
            {
                _levelBuilder.Add(_data.Items[_selectedAction].Item);
                e.Use();
            }

            if (IsClearButtonPressed)
            {
                IsClearButtonPressed = false;
            }
        }

        private void EditorButton()
        {
            _levelBuilder.IsReplaced = EditorGUILayout.Toggle("Replace", _levelBuilder.IsReplaced);
            GUILayout.Space(10);

            var height = _labels.Length > 2 ? LabelScale.y * 1.5f : LabelScale.y;
            var row = (_labels.Length / 2f) * height;
            _selectedAction = GUILayout.SelectionGrid(_selectedAction, _labels, 2, GUILayout.Width(LabelScale.x), GUILayout.Height(row));
            GUILayout.Space(10);

            if (GUILayout.Button("Clear", GUILayout.Width(LabelScale.x), GUILayout.Height(LabelScale.y)))
                _levelBuilder.Clear();
        }

        private void SavePrefab()
        {
            GUILayout.Space(20);
            _levelBuilder.DirectoryPath = EditorGUILayout.TextField("Path", _levelBuilder.DirectoryPath, GUILayout.Width(position.x));
            _levelBuilder.FileName = EditorGUILayout.TextField("FileName", _levelBuilder.FileName, GUILayout.Width(position.x));

            GUILayout.Space(10);

            if (GUILayout.Button("Save", GUILayout.Width(LabelScale.x), GUILayout.Height(LabelScale.y)))
                _levelBuilder.SavePrefab();
        }

        #region DrawGizmos
        private void GizmosInput()
        {
            _gizmo.Grid= EditorGUILayout.Vector3Field("GridSize", _gizmo.Grid);
            GUILayout.Space(10);

            _gizmo.ColorGrid = EditorGUILayout.ColorField("ColorGrid", _gizmo.ColorGrid);
            _gizmo.ColorEmptyCell = EditorGUILayout.ColorField("ColorEmptyCell", _gizmo.ColorEmptyCell);
            _gizmo.ColorFullCell = EditorGUILayout.ColorField("ColorFullCell", _gizmo.ColorFullCell);
            GUILayout.Space(10);

            _gizmo.IsDraw = EditorGUILayout.Toggle("IsDraw", _gizmo.IsDraw);
        }

        private void CreateGizmos()
        {
            DrawGizmos container = FindObjectOfType<DrawGizmos>();

            if (container)
                DestroyImmediate(container.gameObject);

            if (_gizmoContainer == null)
            {
                _gizmoContainer = new GameObject("Gizmos");
                _gizmo = _gizmoContainer.AddComponent<DrawGizmos>();
                _gizmo.LevelBuilder = _levelBuilder;
            }
        }
        #endregion
    }
}