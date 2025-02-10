using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.LevelBuilder
{
    public class JsonConvertor
    {
        public void Save(Level level)
        {
            var list = level.gameObject.GetComponentsInChildren<Item>().ToList();

            var data = new LevelData()
            {
                PlayerSpawnPoint = level.PlayerPosition.Point,
                Cube = GetElement<Cube>(list),
                Cell = GetElement<Cell>(list),
                Coin = GetElement<Coin>(list)
            };
        }

        private List<Vector3> GetElement<T>(List<Item> list) where T : Item
        {
            var point = new List<Vector3>();
            list.FindAll(p => p is T).ForEach(p => point.Add(p.transform.localPosition));

            return point;
        }
    }
}