using UnityEngine;

namespace N19
{
    public static class DataProvider
    {
        private const string PATH = "Data/" + nameof(DefaultData);

        static DataProvider()
        {
            Data = Resources.Load<DefaultData>(PATH);

#if UNITY_EDITOR
            if (Data == null)
            {
                Debug.Log($"Data is null, create defaultData".Color(ColorType.Cyan));
                Data = (DefaultData)ScriptableObject.CreateInstance(nameof(DefaultData));
            }
#endif
        }

        public static DefaultData Data { get; private set; }

        public static void Init() { }
    }
}