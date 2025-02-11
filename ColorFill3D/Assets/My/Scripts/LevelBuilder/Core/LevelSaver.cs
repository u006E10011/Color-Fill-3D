using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Project.LevelBuilder
{
    [CreateAssetMenu(fileName = nameof(LevelSaver), menuName = "Level/" + nameof(LevelSaver))]
    public class LevelSaver : ScriptableObject
    {
        [SerializeField] private string _path = @"Assets/StreamingAssets/";
        [SerializeField] private bool _prettyPrint = true;

        [SerializeField] private List<Level> _level = new();
        [SerializeField] private List<string> _allPath = new();
        [SerializeField] private List<string> _json = new();

        private readonly JsonConvertor _jsonConvertor = new();

        [ContextMenu(nameof(ConvertToJson))]
        public void ConvertToJson()
        {
            foreach (Level level in _level)
                _jsonConvertor.Save(level, _path + level.gameObject.name + ".json", _prettyPrint);
        }

        [ContextMenu(nameof(LoadPathJson))]
        public void LoadPathJson()
        {
            _allPath = _jsonConvertor.LoadAllPath(_path);

            Save();
        }

        [ContextMenu(nameof(GetJson))]
        public void GetJson()
        {
            foreach (var item in _allPath)
                _json.Add(File.ReadAllText(item));

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