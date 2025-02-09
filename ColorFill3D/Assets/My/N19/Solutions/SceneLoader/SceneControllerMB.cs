using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace N19
{
    public class SceneControllerMB : MonoBehaviour
    {
        [SerializeField] private SceneType _type = SceneType.Current;
        [SerializeField] private int _index;

        [SerializeField, Space(10)] private Button _button;

        private void Reset() => _button = _button != null ? _button : GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(Load);
        private void OnDisable() => _button.onClick.RemoveListener(Load);

        private void Load()
        {
             System.Action action = _type switch
            {
                SceneType.Index => () => SceneManager.LoadScene(_index),
                SceneType.Current => () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex),
                SceneType.Next => () => NextScene(),
                _ => default
            };

            action?.Invoke();
        }

        private void NextScene()
        {
            var curentIndex = SceneManager.GetActiveScene().buildIndex;

            if (SceneManager.sceneCountInBuildSettings > curentIndex + 1)
                SceneManager.LoadScene(curentIndex + 1);
        }
    }
}