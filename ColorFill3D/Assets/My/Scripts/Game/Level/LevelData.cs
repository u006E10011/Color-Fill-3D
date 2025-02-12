using System.Collections.Generic;
using UnityEngine;

namespace Project.LevelBuilder
{
    [System.Serializable]
    public class LevelData
    {
        public Vector3 PlayerSpawnPoint;
        public List<Vector3> Cube = new();
        public List<Vector3> Cell = new();
        public List<Vector3> Coin = new();
    }
}