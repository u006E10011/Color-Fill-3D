using Project.LevelBuilder;
using UnityEngine;
using YG;

namespace Project
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelBuilderData _levelBuilderData;
        [SerializeField] private LevelSaver _levelSaver;

        [SerializeField] private string _path = "Level";

        [SerializeField] private float _step = 30;
        [SerializeField] private float _offsetY = .5f;

        [SerializeField, Space(10)] private float _waitDestroyLevel = 3f;

        public Level CurrentLevel { get; private set; }

        private LevelData _levelData;
        private float _nextPosition;
        private int _index;

        private void OnEnable() => EventBus.Instance.OnCompleteLevel += Complete;
        private void OnDisable() => EventBus.Instance.OnCompleteLevel -= Complete;

        public void Init()
        {
            _levelData = new JsonConvertor().Load(_path);
            _index = YandexGame.savesData.LevelIndex;

            GetLevel();
        }

        private void Complete()
        {
            //if (_index > _levels.Count - 1)
            //    Debug.LogError($"The index ({_index}) has exceeded the maximum number of levels ({_levels.Count}). The last level will load");

            try
            {
                _index++;
            }
            catch (System.Exception)
            {
                _index--;
            }


            YandexGame.savesData.LevelIndex = _index;
            YandexGame.SaveProgress();

            Destroy(CurrentLevel.gameObject, _waitDestroyLevel);
            GetLevel();
        }

        private void GetLevel()
        {
            var position = Vector3.up * _offsetY + Vector3.forward * _nextPosition;
            CurrentLevel = CreateLevel();
            _nextPosition += _step;

            EventBus.Instance.OnNextLevel?.Invoke();
            EventBus.Instance.OnMoveCamera?.Invoke(CurrentLevel);
            EventBus.Instance.OnMovePlayer?.Invoke(CurrentLevel.PlayerPosition.Point);
        }

        private Level CreateLevel()
        {
            var level = new GameObject("Level").AddComponent<Level>();
            var spawnPoint = new GameObject("SpawnPoint").AddComponent<PlayerSpawnPoint>();

            for (int i = 0; i < _levelData.Cube.Count; i++)
                Instantiate(_levelBuilderData.Items[0].Item, _levelData.Cube[i], Quaternion.identity, level.transform);
            for (int i = 0; i < _levelData.Cell.Count; i++)
                Instantiate(_levelBuilderData.Items[1].Item, _levelData.Cell[i], Quaternion.identity, level.transform);

            level.PlayerPosition = spawnPoint;
            spawnPoint.transform.parent = level.transform;
            spawnPoint.SetPosition(_levelData.PlayerSpawnPoint);
            return level;
        }
    }
}