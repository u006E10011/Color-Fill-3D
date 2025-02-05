using System.Collections.Generic;
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
            _levelBuilder.IsReplaced = EditorGUILayout.Toggle("Replace", _levelBuilder.IsReplaced);
            GUILayout.Space(10);

            var label = new Vector2(position.width - 5, 30);

            _selectedAction = GUILayout.SelectionGrid(_selectedAction, _labels, 2, GUILayout.Width(label.x), GUILayout.Height(label.y));
            GUILayout.Space(10);

            if (GUILayout.Button("Clear", GUILayout.Width(label.x), GUILayout.Height(label.y)))
                _levelBuilder.Clear();
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
    }
}