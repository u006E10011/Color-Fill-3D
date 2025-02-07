using UnityEditor;
using UnityEngine;

namespace Project.LevelBuilder
{
    public static class LevelBuilderInput
    {
        public static bool IsMouseLeftPressed { get; private set; }
        public static bool IsMouseLeftClick { get; private set; }

        public static bool IsMouseRightPressed { get; private set; }
        public static bool IsMouseRightClick { get; private set; }

        public static bool IsCTRLPressed { get; private set; }
        public static bool IsClearButtonPressed;

        public static void Update(Event e)
        {
            MouseLeft(e);
            MouseRight(e);
            CTRL(e);

        }

        private static void MouseLeft(Event e)
        {
            IsMouseLeftClick = e.type == EventType.MouseDown && e.button == 0;

            if (e.type == EventType.MouseDown && e.button == 0)
                IsMouseLeftPressed = true;
            else if (e.type == EventType.MouseUp && e.button == 0)
                IsMouseLeftPressed = false;
        }

        private static void MouseRight(Event e)
        {
            IsMouseRightClick = e.type == EventType.MouseDown && e.button == 1;

            if (e.type == EventType.MouseDown && e.button == 1)
                IsMouseRightPressed = true;
            else if (e.type == EventType.MouseUp && e.button == 1)
                IsMouseRightPressed = false;
        }

        private static void CTRL(Event e)
        {
            IsCTRLPressed = e.control;
        }
    }
}