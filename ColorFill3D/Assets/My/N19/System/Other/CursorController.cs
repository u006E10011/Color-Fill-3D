using UnityEngine;

namespace N19
{
    public static class CursorController
    {
        public static bool Visible { get; private set; }

        public static void IsVisible(bool isVisible)
        {
            Visible = isVisible;

            Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isVisible;
        }

        public static void Switch()
        {
            Visible = !Visible;

            Cursor.lockState = Visible ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = Visible;
        }
    }
}