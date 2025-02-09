using N19;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = nameof(SetFontAllData), menuName = "N19/Translate/" + nameof(SetFontAllData))]
    public class SetFontAllData : ScriptableObject
    {
        [Tooltip("Применять ли сразу тексту шрифт")]
        public bool IsAutoSet = true;

        [Tooltip("Список, у которых нужно изменить шрифт. Используйте метод SetFontAll из контекстного меню для замены шрифта (стоит после этого сохранится (Ctrl + S))")]
        public List<TranslateData> TranslateData;

        [Tooltip("Шрифт, который применится ко всем конфигам с переводом, которые лежат в списке TranslateConfigs")]
        [Space(10)] public TMP_FontAsset FontAsset;

        private void OnValidate()
        {
            if (IsAutoSet)
                SetTextFont();
        }

        [ContextMenu(nameof(GetData))]
        public void GetData()
        {
            TranslateData = new(GetAllTranslateDataAssets());
        }

        [ContextMenu(nameof(SetFontAll))]
        public void SetFontAll()
        {
            foreach (var config in TranslateData)
            {
                config.Font = FontAsset;

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(config);
#endif
            }

            SetTextFont();
        }

        private void SetTextFont()
        {
            FindObjectsOfType<TMP_Text>(true).ForEach(p => p.font = FontAsset);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

        private List<TranslateData> GetAllTranslateDataAssets()
        {
            List<TranslateData> allTranslateData = new List<TranslateData>();

#if UNITY_EDITOR
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(TranslateData).Name);

            foreach (string guid in guids)
            {
                string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                TranslateData translateDataAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TranslateData>(assetPath);

                if (translateDataAsset != null)
                    allTranslateData.Add(translateDataAsset);
            }
#endif
            return allTranslateData;
        }
    }
}