#if UNITY_EDITOR

using UnityEngine;
using System;

namespace N19
{
    public class GameObjectOfTypeAttribute : PropertyAttribute
    {
        public Type Type { get; }
        public bool AllowSceneObject { get; }

        public GameObjectOfTypeAttribute(Type requeredType, bool allowSceneObject = true)
        {
            Type = requeredType;
            AllowSceneObject = allowSceneObject;
        }
    }
}
#endif