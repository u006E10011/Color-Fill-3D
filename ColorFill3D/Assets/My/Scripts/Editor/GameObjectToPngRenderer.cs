using System.IO;
using UnityEditor;
using UnityEngine;

public class GameObjectToPngRenderer : EditorWindow
{
    private GameObject _objectToRender;
    private int _resolution = 2048;
    private Texture2D _previewTexture;
    private string _lastSavePath = "";

    [MenuItem("Tools/GameObject To Png Renderer")]
    public static void ShowWindow()
    {
        GetWindow<GameObjectToPngRenderer>("GameObject To Png Renderer");
    }

    private void OnGUI()
    {
        GUILayout.Label("GameObject To Png Renderer", EditorStyles.boldLabel);

        _objectToRender = (GameObject)EditorGUILayout.ObjectField("Object to Render", _objectToRender, typeof(GameObject), true);
        _resolution = EditorGUILayout.IntField("Resolution", _resolution);

        if (GUILayout.Button("Render Preview"))
        {
            if (_objectToRender == null)
            {
                Debug.LogWarning("No object selected for rendering.");
                return;
            }
            RenderPreview();
        }

        if (_previewTexture != null)
        {
            GUILayout.Label("Preview:");
            GUILayout.Box(_previewTexture, GUILayout.Width(256), GUILayout.Height(256));
        }

        if (_previewTexture != null && GUILayout.Button("Save Preview"))
        {
            SavePreview();
        }

        GUILayout.Space(10);

        if (!string.IsNullOrEmpty(_lastSavePath))
        {
            GUILayout.Label($"Last Save Path: {_lastSavePath}");
            if (GUILayout.Button("Reset Save Path"))
            {
                _lastSavePath = "";
                Debug.Log("Save path reset.");
            }
        }
    }

    private void RenderPreview()
    {
        Renderer[] renderers = _objectToRender.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.LogWarning($"No renderers found in {_objectToRender.name} or its children.");
            return;
        }

        Bounds bounds = renderers[0].bounds;
        foreach (var renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        GameObject tempCameraObject = new GameObject("TempCamera");
        Camera camera = tempCameraObject.AddComponent<Camera>();

        camera.transform.position = bounds.center - Vector3.forward * bounds.size.magnitude;
        camera.transform.LookAt(bounds.center);
        camera.orthographic = true;
        camera.orthographicSize = Mathf.Max(bounds.size.x, bounds.size.y) / 2;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0, 0, 0, 0);
        RenderTexture renderTexture = new RenderTexture(_resolution, _resolution, 24, RenderTextureFormat.ARGB32);
        renderTexture.enableRandomWrite = true;
        RenderTexture.active = renderTexture;
        camera.targetTexture = renderTexture;

        // Rendering
        Texture2D screenshot = new Texture2D(_resolution, _resolution, TextureFormat.RGBA32, false);
        camera.Render();
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, _resolution, _resolution), 0, 0);
        screenshot.Apply();

        _previewTexture = screenshot;

        RenderTexture.active = null;
        camera.targetTexture = null;
        DestroyImmediate(renderTexture);
        DestroyImmediate(tempCameraObject);

        Debug.Log("Preview successfully.");
    }

    private void SavePreview()
    {
        if (string.IsNullOrEmpty(_lastSavePath))
        {
            _lastSavePath = EditorUtility.OpenFolderPanel("Select Folder to Save PNG", "", "");
        }

        if (string.IsNullOrEmpty(_lastSavePath)) return;

        string filePath = Path.Combine(_lastSavePath, $"{_objectToRender.name}_Preview.png");
        byte[] pngData = _previewTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, pngData);
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();

        Debug.Log($"Preview saved at {filePath}");
    }
}
