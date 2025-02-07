using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

namespace Project
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private string _path = "Level";
        [SerializeField] private float _step = 30;
        [SerializeField] private float _waitDestroyLevel = 3f;

        private float _nextPosition;
        private int _index;

        private List<Level> _levels = new();
        private Level _currentLevel;

        private void OnEnable() => EventBus.Instance.OnCompleteLevel += Complete;
        private void OnDisable() => EventBus.Instance.OnCompleteLevel -= Complete;

        public void Init()
        {
            _levels = Resources.LoadAll<Level>(_path).ToList();
            _index = YandexGame.savesData.LevelIndex;

            GetLevel();
        }

        private void Complete()
        {
            _index++;

            Destroy(_currentLevel, _waitDestroyLevel);
            GetLevel();
        }

        private void GetLevel()
        {
            _currentLevel = Instantiate(_levels[_index], Vector3.forward * _nextPosition, Quaternion.identity);
            _nextPosition += _step;

            EventBus.Instance.OnNextLevel?.Invoke();
        }
    }
}