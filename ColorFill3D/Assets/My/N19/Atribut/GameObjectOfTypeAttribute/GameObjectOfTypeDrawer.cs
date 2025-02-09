#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace N19
{
    [CustomPropertyDrawer(typeof(GameObjectOfTypeAttribute))]
    public class GameObjectOfTypeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsFieldGameObject())
            {
                DrawError(position);
            }

            var gootAttribute = attribute as GameObjectOfTypeAttribute;
            var requiredType = gootAttribute.Type;

            CheckDragDrop(position, requiredType);
            DrawObjectField(property, position, label, gootAttribute.AllowSceneObject);
        }

        private bool IsFieldGameObject()
        {
            return fieldInfo.FieldType == typeof(GameObject) || typeof(IEnumerable<GameObject>).IsAssignableFrom(fieldInfo.FieldType);
        }

        private void DrawError(Rect position)
        {
            EditorGUI.HelpBox(position, "<color=green>GameObjectOfTypeAttribute</color> работает только лишь с ссылками на <color=green>GameObject</color>", MessageType.Error);
        }

        private void CheckDragDrop(Rect position, Type requiredType)
        {
            if (position.Contains(Event.current.mousePosition))
            {
                var draggerObjectsCount = DragAndDrop.objectReferences.Length;

                for (int i = 0; i < draggerObjectsCount; i++)
                {
                    if (!IsValidObject(DragAndDrop.objectReferences[i], requiredType))
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                        break;
                    }
                }
            }
        }

        private bool IsValidObject(UnityEngine.Object objectReferences, Type requiredType)
        {
            bool result = false;

            var go = objectReferences as GameObject;

            if(go != null)
                result = go.GetComponent(requiredType) != null;

            return result;
        }

        private void DrawObjectField(SerializedProperty property, Rect position, GUIContent label, bool allowSceneObjects)
        {
            property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(GameObject), allowSceneObjects);
        }
    }
}
#endif