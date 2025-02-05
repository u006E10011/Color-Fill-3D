using UnityEngine;

namespace Project
{
    public class Paint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Brush.Paint(other.gameObject);
        }
    }
}