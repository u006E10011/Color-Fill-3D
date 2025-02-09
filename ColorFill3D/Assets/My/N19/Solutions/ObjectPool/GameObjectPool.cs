using UnityEngine;

namespace N19
{
    public class GameObjectPool : PoolBase<GameObject>
    {
        public GameObjectPool(GameObject prefab, int preloadCount) 
            : base(() => PreloadAction(prefab), GetAction, ReturnAction, preloadCount) { }

        public static GameObject PreloadAction(GameObject prefab) => Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        public static void GetAction(GameObject prefab) => prefab.Activate();
        public static void ReturnAction(GameObject prefab) => prefab.Deactivate();
    }
}