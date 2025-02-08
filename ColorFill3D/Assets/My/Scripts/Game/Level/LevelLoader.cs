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
        [SerializeField] private float _offsetY = .5f;

        [SerializeField, Space(10)] private float _waitDestroyLevel = 3f;

        public Level CurrentLevel { get; private set; }

        private float _nextPosition;
        private int _index;
        private List<Level> _levels = new();

        private void OnEnable() => EventBus.Instance.OnCompleteLevel += Complete;
        private void OnDisable() => EventBus.Instance.OnCompleteLevel -= Complete;

        public void Init()
        {
            _levels = Resources.LoadAll<Level>(_path)
                .OrderBy(p => int.Parse(p.name.Replace("Level_", "")))
                .ToList();
        
            _index = YandexGame.savesData.LevelIndex;

            GetLevel();
        }

        private void Complete()
        {
            _index++;

            YandexGame.savesData.LevelIndex++;
            YandexGame.SaveProgress();

            Destroy(CurrentLevel.gameObject, _waitDestroyLevel);
            GetLevel();
        }

        private void GetLevel()
        {
            if (_index > _levels.Count - 1)
            {
                Debug.LogError($"The index ({_index}) has exceeded the maximum number of levels ({_levels.Count}). The last level will load");
                _index = _levels.Count - 1;
            }

            var position = Vector3.up * _offsetY + Vector3.forward * _nextPosition;
            CurrentLevel = Instantiate(_levels[_index], position, Quaternion.identity);
            _nextPosition += _step;

            EventBus.Instance.OnNextLevel?.Invoke();
            EventBus.Instance.OnMoveCamera?.Invoke(CurrentLevel);
            EventBus.Instance.OnMovePlayer?.Invoke(CurrentLevel.PlayerPosition.Point);
        }
    }
}