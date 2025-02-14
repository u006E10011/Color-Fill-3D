using UnityEngine;

namespace Project
{
    public class Menu : MonoBehaviour
    {
        private PlayerController _player;

        private void OnEnable() => Switch(true);
        private void OnDisable() => Switch(false);

        public void Init(PlayerController player)
        {
            _player = player;
        }

        private void Switch(bool isActive)
        {
            Time.timeScale = isActive ? 0 : 1;
            _player.enabled = !isActive;
        }
    }
}