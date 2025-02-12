using UnityEngine;

namespace Project
{
    public class Coin : LevelBuilder.Item
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<Player>())
                Destroy(gameObject);
        }
    }
}