using System.Collections.Generic;
using UnityEngine;

namespace Project.LevelBuilder
{
    [CreateAssetMenu(fileName = nameof(LevelBuilderData), menuName = "Data/" + nameof(LevelBuilderData))]
    public class LevelBuilderData : ScriptableObject
    {
        public int Layer;
        public float PositionY;

        [field: SerializeField] public List<ItemData> Items { get; private set; } = new();
    }
}