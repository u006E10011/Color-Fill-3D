using UnityEngine;

namespace N19
{
    public static class ObjectExtern
    {
        public static void Activate(this GameObject gameObject) => gameObject.SetActive(true);
        public static void Deactivate(this GameObject gameObject) => gameObject.SetActive(false);

        public static void Activate(this ParticleSystem particleSystem) => particleSystem.gameObject.SetActive(true);
        public static void Deactivate(this ParticleSystem particleSystem) => particleSystem.gameObject.SetActive(false);
    }
}