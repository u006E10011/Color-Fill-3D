using UnityEngine;
using YG;

namespace Project
{
    public class Coin : LevelBuilder.Item
    {
        [SerializeField] private int _value = 25;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                YandexGame.savesData.Bank += _value;
                Destroy(gameObject);
            }
        }
    }
}