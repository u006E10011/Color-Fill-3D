using System;
using UnityEngine;

namespace N19
{
    public static class PauseController
    {
        public static event Action OnPauseOn;
        public static event Action OnPauseOff;

        public static Pause Value { get; private set; } = Pause.Off;

        public static void SwitchPause()
        {
            var action = Value == Pause.On ? Off :(Action)On;
            action?.Invoke();
        }

        public static void SetState(Pause pause)
        {
            Action action = pause switch
            {
                Pause.On => () => On(),
                Pause.Off => () => Off(),
                _ => () => Off(),
            };

            action?.Invoke();
        }

        private static void On()
        {
            Time.timeScale = 0;

            OnPauseOn?.Invoke();
        }

        private static void Off()
        {
            Time.timeScale = 1;

            OnPauseOff?.Invoke();
        }
    }

}