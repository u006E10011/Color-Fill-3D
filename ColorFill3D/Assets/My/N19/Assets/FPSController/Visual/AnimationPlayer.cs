using UnityEngine;

using static N19.Constant;

namespace N19
{
    public class AnimationPlayer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private void Reset() => _animator = _animator != null ? _animator : GetComponent<Animator>();

        private void OnEnable()
        {
            PlayerControllerEvent.OnIdle += Idle;
            PlayerControllerEvent.OnWalk += Move;
            PlayerControllerEvent.OnJumpUp += Jump;
        }

        private void OnDisable()
        {
            PlayerControllerEvent.OnIdle -= Idle;
            PlayerControllerEvent.OnWalk -= Move;
            PlayerControllerEvent.OnJumpUp -= Jump;
        }

        private void Idle() => _animator.SetBool(RUN_ANIMATION, false);
        private void Move() => _animator.SetBool(RUN_ANIMATION, true);
        private void Jump() => _animator.SetTrigger(JUMP_ANIMATION);
    }
}