using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Brush
    {
        private readonly static Dictionary<GameObject, Cell> _cell = new();

        public static void Add(GameObject key, Cell cell) => _cell.Add(key, cell);

        public static void Remove(GameObject key)
        {
            if( _cell.ContainsKey(key))
                _cell.Remove(key);
        }

        public static void Paint(GameObject key)
        {
            if(_cell.TryGetValue(key, out var cell))
            {
                cell.SetColor();
                Remove(key);
            }
        }

        public static int Count() => _cell.Count;
    }
}