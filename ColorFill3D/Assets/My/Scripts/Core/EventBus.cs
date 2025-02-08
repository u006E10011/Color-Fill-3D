﻿using System;
using UnityEngine;

namespace Project
{
    public class EventBus
    {
        private static EventBus _instance;
        public static EventBus Instance => _instance ??= new();

        public Action OnCompleteLevel;
        public Action OnNextLevel;

        public Action<Level> OnMoveCamera;
        public Action<Vector3> OnMovePlayer;

        public Action OnUpdateProgress;
    }
}