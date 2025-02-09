#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

namespace N19
{
    public class ConstantsGeneration
    {
        private const string _outputPath = "Assets/My/N19/System/Constant/";

        [MenuItem("Tools/GenerateUnityConstants")]
        public static void GenerateConstants()
        {
            GenerateSceneConstants();
            GenerateLayerConstants();
            GenerateTagConstants();
        }

        private static void GenerateSceneConstants()
        {
            string classContent = "namespace N19\n{\n    public static class ConstantScene\n    {\n";
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    string sceneName = Path.GetFileNameWithoutExtension(scene.path);
                    classContent += $"        public const string {Filter(sceneName)} = \"{sceneName}\";\n";
                }
            }
            classContent += "    }\n}\n";
            WriteToFile("ConstantScene.cs", classContent);
        }

        private static void GenerateLayerConstants()
        {
            string classContent = "namespace N19\n{\n    public static class ConstantLayer\n    {\n";
            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                int layerIndex = LayerMask.NameToLayer(layerName);

                if (!string.IsNullOrEmpty(layerName))
                {
                    classContent += $"        public const string {Filter(layerName)} = \"{layerIndex}\";\n";
                }
            }
            classContent += "    }\n}\n";
            WriteToFile("ConstantLayer.cs", classContent);
        }

        private static void GenerateTagConstants()
        {
            string classContent = "namespace N19\n{\n    public static class ConstantTag\n    {\n";
            foreach (string tag in UnityEditorInternal.InternalEditorUtility.tags)
            {
                classContent += $"        public const string {Filter(tag)} = \"{tag}\";\n";
            }
            classContent += "    }\n}\n";
            WriteToFile("ConstantTag.cs", classContent);
        }

        private static string Filter(string name)
        {
            string result = name.Replace(" ", string.Empty);

            if (char.IsDigit(result[0]))
                result = "N" + result;

            return result;
        }

        private static void WriteToFile(string fileName, string content)
        {
            if (!Directory.Exists(_outputPath))
            {
                Directory.CreateDirectory(_outputPath);
            }
            File.WriteAllText(_outputPath + fileName, content);
            AssetDatabase.Refresh();
        }
    }
}
#endif