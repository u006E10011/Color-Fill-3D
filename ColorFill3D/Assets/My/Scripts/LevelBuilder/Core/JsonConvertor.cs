using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Project.LevelBuilder
{
    public class JsonConvertor
    {
        public void Save(Level level, string path, bool prettyPrint)
        {
            var list = level.gameObject.GetComponentsInChildren<Item>().ToList();

            var data = new LevelData()
            {
                PlayerSpawnPoint = level.PlayerPosition.Point,
                Cube = GetElement<Cube>(list),
                Cell = GetElement<Cell>(list),
                Coin = GetElement<Coin>(list)
            };

            var directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);

                if(!Directory.Exists(directoryPath))
                {
                    Debug.LogError($"Invalid directory path: {path}");
                    return;
                }
            }

            string json = JsonUtility.ToJson(data, prettyPrint);
            File.WriteAllText(path, json);

#if UNITY_EDITOR
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
#endif
        }

        public LevelData Load(string path)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Debug.LogError($"Invalid directory path: {path}");
                    return null;
                }

                var json = File.ReadAllText(path);
                return JsonUtility.FromJson<LevelData>(json);
            }
            catch(System.Exception e)
            {
                Debug.LogError("Load exception: " + e.Message);
                return null;
            }
        }

        public List<string> LoadAllPath(string path)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Debug.LogError($"Invalid directory path: {path}");
                    return null;
                }

                return Directory.GetFiles(Path.GetDirectoryName(path), "*.json").ToList();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Load All Files: " + e.Message);
                return null;
            }
        }

        private List<Vector3> GetElement<T>(List<Item> list) where T : Item
        {
            var point = new List<Vector3>();
            list.FindAll(p => p is T).ForEach(p => point.Add(p.transform.localPosition));

            return point;
        }
    }
}