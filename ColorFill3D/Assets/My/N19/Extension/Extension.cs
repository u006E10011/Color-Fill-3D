using System;
using System.Collections.Generic;
using UnityEngine;

namespace N19
{
    public static class Extension 
    {
        #region List
        public static List<T> ForEach<T>(this List<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);

            return collection;
        }

        public static T RandomItem<T>(this List<T> collection)
        {
            var item = UnityEngine.Random.Range(0, collection.Count);
            return collection[item];
        }
        #endregion

        #region Array
        public static T[] ForEach<T>(this T[] collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);

            return collection;
        }

        public static T RandomItem<T>(this T[] collection)
        {
            var item = UnityEngine.Random.Range(0, collection.Length);
            return collection[item];
        }
        #endregion

        #region Transform
        public static void ForEach<T>(this Transform collection, Action<Transform> action) where T : Transform
        {
            foreach (Transform item in collection)
                action(item);
        }
        #endregion
    }
}