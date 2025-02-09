using UnityEngine;

namespace N19
{
    [CreateAssetMenu(fileName = nameof(DefaultData), menuName = "N19/" + nameof(DefaultData))]
    public class DefaultData : ScriptableObject
    {
        public int TargetFrameRate = 60;
        public bool VisibleCursor = false;
        public bool LockInput = false;
    }
}