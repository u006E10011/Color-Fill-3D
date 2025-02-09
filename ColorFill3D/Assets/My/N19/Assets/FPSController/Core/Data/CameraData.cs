using UnityEngine;

namespace N19
{
    public enum CameraType : byte
    {
        FPS,
        TPS_V1,
        TPS_V2,
    }

    [CreateAssetMenu(fileName = nameof(CameraData), menuName = "N19/PlayerController/" + nameof(CameraData))]
    public class CameraData : ScriptableObject
    {
        [Tooltip(
            "FPS - Камера от первого лица\n\n"
            + "TPS_V1 - Камера от 3 лица\n"
            + "- Свободный осмотр\n"
            + "- Движение относительно модели\n\n"
            + "TPS_V2 - Камера от 3 лица:\n"
            + "- Свободный осмотр\n"
            + "- Движение относительно камеры\n")]

        public CameraType CameraType;
        public float SpeedRotationModel = 10;

        [Header("Mouse")]
        public float Sensitivity = 200;
        public MinMax RotateMinMax = new(-90, 90);
    }
}

