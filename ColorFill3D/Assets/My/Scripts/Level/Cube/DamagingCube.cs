using UnityEngine;

namespace Project
{
    public class DamagingCube : Cube
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                Destroy(other.gameObject);
                Debug.Log("Destroy player");
            }
        }
    }
}