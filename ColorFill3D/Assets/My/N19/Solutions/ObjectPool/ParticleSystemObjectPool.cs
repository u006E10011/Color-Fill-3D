using UnityEngine;

namespace N19
{
    public class ParticleSystemObjectPool : PoolBase<ParticleSystem>
    {
        public ParticleSystemObjectPool(ParticleSystem prefab, int preloadCount)
            : base(() => PreloadAction(prefab), GetAction, ReturnAction, preloadCount) { }

        public static ParticleSystem PreloadAction(ParticleSystem prefab) => Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        public static void GetAction(ParticleSystem prefab) => prefab.Activate();
        public static void ReturnAction(ParticleSystem prefab) => prefab.Deactivate();
    }
}