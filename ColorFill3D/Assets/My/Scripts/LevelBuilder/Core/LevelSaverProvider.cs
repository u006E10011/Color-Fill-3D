using System.Collections.Generic;
using UnityEngine;

namespace Project.LevelBuilder
{
    [CreateAssetMenu(fileName = nameof(LevelSaverProvider), menuName = "Level/" + nameof(LevelSaverProvider))]
    public class LevelSaverProvider : ScriptableObject
    {
        [SerializeField] private string _path = @"Assets/StreamingAssets/";
        [SerializeField] private bool _prettyPrint = true;

        [SerializeField] private List<Level> _level = new();
        [SerializeField] private List<LevelData> _levelData = new();

        public List<LevelData> Data => _levelData;

        private readonly JsonConvertor _jsonConvertor = new();

        [ContextMenu(nameof(ConvertToJson))]
        public void ConvertToJson()
        {
            foreach (Level level in _level)
                _jsonConvertor.Save(level, _path + level.gameObject.name + ".json", _prettyPrint);
        }

        [ContextMenu(nameof(LoadJson))]
        public void LoadJson()
        {
            var path = _jsonConvertor.LoadAllPath(_path);

            foreach (var item in path)
            {
                var data = _jsonConvertor.Load(item);
                _levelData.Add(data);
            }

            Save();
        }

        private void Save()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.Refresh();
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }
    }
}