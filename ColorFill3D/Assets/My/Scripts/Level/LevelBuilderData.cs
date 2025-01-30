using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = nameof(LevelBuilderData), menuName = "Data/" + nameof(LevelBuilderData))]
    public class LevelBuilderData : ScriptableObject
    {
        public List<Cube> Cube;

        public float PositionY = .5f;
        public LayerMask Layer = 6;
    }
}