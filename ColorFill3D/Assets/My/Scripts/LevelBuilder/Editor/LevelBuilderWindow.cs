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

        private string _path = @"Assets/My/Prefab/Resources/Level";

        private Vector2 LabelScale => new(position.width - 5, 30);

        private LevelBuilder _levelBuilder;
        private LevelBuilderData _data;

        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;

            _data = AssetDatabase.LoadAssetAtPath(@"Assets/My/Data/LevelBuilderData.asset", typeof(LevelBuilderData)) as LevelBuilderData;
            _levelBuilder = new();
            _labels = _data.Items.Select(p => p.name).ToArray();
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
            var mouseRight = IsMouseRightPressed ||IsMouseRightClick;

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

            _selectedAction = GUILayout.SelectionGrid(_selectedAction, _labels, 2, GUILayout.Width(LabelScale.x), GUILayout.Height(LabelScale.y));
            GUILayout.Space(10);

            if (GUILayout.Button("Clear", GUILayout.Width(LabelScale.x), GUILayout.Height(LabelScale.y)))
                _levelBuilder.Clear();
        }

        private void SavePrefab()
        {
            GUILayout.Space(20);
            _path = EditorGUILayout.TextField("Path", _path, GUILayout.Width(position.x));

            GUILayout.Space(10);

            if (GUILayout.Button("Save", GUILayout.Width(LabelScale.x), GUILayout.Height(LabelScale.y)))
                _levelBuilder.SavePrefab(_path);
        }
    }
}