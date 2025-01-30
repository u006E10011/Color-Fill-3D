using System.Collections;
using UnityEngine;

namespace Project
{
    public class CatchPoint : Cube
    {
        [SerializeField] private float _durationAnimation = .3f;
        [SerializeField] private Vector3 _targetScale = Vector3.zero;

        [SerializeField, Space(10)] private Transform mesh;

        private void Awake()
        {
            mesh.localScale = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
                StartCoroutine(DOScale());
        }

        private IEnumerator DOScale()
        {
            var timer = 0f;

            while (_durationAnimation > timer)
            {
                timer += Time.deltaTime;
                mesh.localScale = Vector3.Lerp(mesh.localScale, _targetScale, timer / _durationAnimation);

                yield return null;
            }
        }
    }
}