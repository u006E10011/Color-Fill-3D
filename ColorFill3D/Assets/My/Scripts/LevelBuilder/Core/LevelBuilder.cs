using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Project.LevelBuilder
{
    [ExecuteInEditMode]
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private LevelBuilderData _data;
        [SerializeField, Space(10)] private Camera _camera;

        [SerializeField, Space(10)] private bool _isReplaced;

        private int _index;

        private readonly Dictionary<Vector3, Item> _items = new();
        private readonly List<Transform> _container = new();

        private void OnEnable() => EditorApplication.update += EditorUpdate;
        private void OnDisable() => EditorApplication.update -= EditorUpdate;

        private void EditorUpdate()
        {
            if (Application.isPlaying)
                return;

            Scroll();
            InputHandler();
        }

        #region InputHandler
        private void Scroll()
        {
            var scroll = Input.GetAxisRaw("Mouse ScrollWheel");

            if (scroll > 0)
                _index++;
            else if (scroll < 0)
                _index--;

            if (_index > _data.Items.Count - 1)
                _index = 0;
            else if (_index < 0)
                _index = _data.Items.Count - 1;
        }

        private void InputHandler()
        {
            var input = Event.current;

            if (input.isMouse)
            {
                if (input.button == 0)
                    Add();

                if (input.button == 1)
                    Remove();
            }
        }
        #endregion

        #region Core
        private void Remove()
        {
            if (GetPoint(out var point))
            {
                if (Exist(point))
                {
                    DestroyImmediate(_items[point].gameObject);
                    _items.Remove(point);
                }
            }
        }
        private void Add()
        {
            var parent = _container.Count > _index ? _container[_index] : null;

            if (GetPoint(out var point))
            {
                if (!_container.Contains(parent))
                {
                    parent = new GameObject(_data.Items[_index].name).transform;
                    parent.parent = transform;
                    _container.Add(parent);
                }

                if (_isReplaced && Exist(point))
                {
                    DestroyImmediate(_items[point].gameObject);
                    _items.Remove(point);

                    _items[point] = Instantiate(_data.Items[_index].Item, point, Quaternion.identity, parent);
                }
                else if (!Exist(point))
                    _items[point] = Instantiate(_data.Items[_index].Item, point, Quaternion.identity, parent);
            }
        }
        #endregion

        #region Data
        [ContextMenu(nameof(ResetData))]
        public void ResetData()
        {
            foreach (var item in _container)
                DestroyImmediate(item?.gameObject);

            _items.Clear();
            _container.Clear();
        }

        private bool Exist(Vector3 position)
        {
            return _items.ContainsKey(position);
        }

        private bool GetPoint(out Vector3 position)
        {
            var result = Physics.Raycast(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition), out var hitInfo, Mathf.Infinity);
            //var result = Physics.Raycast(_camera.ScreenPointToRay(Event.current.mousePosition), out var hitInfo, Mathf.Infinity);
            position = new(Mathf.Round(hitInfo.point.x), _data.PositionY, Mathf.Round(hitInfo.point.z));
            return result;
        }
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (enabled == false)
                return;

            var scale = _data.Items[_index].Item.gameObject.transform.localScale;

            if (GetPoint(out var point))
            {
                if (!Exist(point))
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(point, scale);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(point, scale);
                }
            }
        }
#endif
    }
}