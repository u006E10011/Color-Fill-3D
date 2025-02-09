using UnityEngine;

namespace N19
{
    public class InputPlayer : MonoBehaviour
    {
        #region Instance
        private static IInputPlayer _instance;
        public static IInputPlayer Instance
        {
            get => _instance;
            set => _instance = value;
        }
        #endregion

        public static bool IsLockInput { get; set; }

        [SerializeField] private PlayerControllerData _data;

        #region Mobile
        [SerializeField] private Canvas _inputMobile;
        [SerializeField] private Joystick _joystick;

        [SerializeField, Space(10)] private SelectButton _jumpButton;
        #endregion

        private void Awake()
        {
            _instance = PlatformController.Instance.Type == PlatformType.PC
                ? new InputPC(_data)
                : new InputMobile(_joystick, _jumpButton, _inputMobile);

#if UNITY_EDITOR
            Debug.Log("IsMobile: ".Color(ColorType.Cyan) + YG.YandexGame.EnvironmentData.isMobile.Color(ColorType.Magenta));
            Debug.Log("Platform: ".Color(ColorType.Cyan) + PlatformController.Instance.Type.Color(ColorType.Magenta));
#endif
        }

        private void Update()
        {
            if (!IsLockInput)
                _instance.Update();
            else
                _instance.Reset();
        }
    }
}