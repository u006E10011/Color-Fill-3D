using UnityEngine;
using UnityEngine.UI;

namespace N19
{
    public class SetPlatformButton : MonoBehaviour
    {
        [SerializeField] private Button PC;
        [SerializeField] private Button _mobile;

        private void OnEnable()
        {
            PC.onClick.AddListener(SetPC);
            _mobile.onClick.AddListener(SetMobile);
        }

        private void OnDisable()
        {
            PC.onClick.RemoveListener(SetPC);
            _mobile.onClick.RemoveListener(SetMobile);
        }

        private void SetPC() => PlatformController.Instance.Set(PlatformType.PC);
        private void SetMobile() => PlatformController.Instance.Set(PlatformType.Mobile);
    }
}