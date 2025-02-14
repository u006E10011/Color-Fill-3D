using Project.LevelBuilder;
using UnityEngine;
using YG;

namespace Project
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelBuilderData _levelBuilderData;
        [SerializeField] private LevelSaverProvider _levelDataProvider;

        [SerializeField] private float _step = 30;
        [SerializeField] private float _offsetY = .5f;

        [SerializeField, Space(10)] private float _waitDestroyLevel = 3f;

        public Level CurrentLevel { get; private set; }

        private float _nextPosition;
        private int _index;

        private void OnEnable() => EventBus.Instance.OnCompleteLevel += Complete;
        private void OnDisable() => EventBus.Instance.OnCompleteLevel -= Complete;

        public void Init()
        {
            _index = YandexGame.savesData.LevelIndex;
            GetLevel();
        }

        private void Complete()
        {
            if (_index > _levelDataProvider.Data.Count - 1)
                Debug.LogError($"The index ({_index}) has exceeded the maximum number of levels ({_levelDataProvider.Data.Count}). The last level will load");

            _index++;
            _index = Mathf.Clamp(_index++, 0, _levelDataProvider.Data.Count - 1);


            YandexGame.savesData.LevelIndex = _index;
            YandexGame.SaveProgress();

            Destroy(CurrentLevel.gameObject, _waitDestroyLevel);
            GetLevel();
        }

        private void GetLevel()
        {
            var position = Vector3.up * _offsetY + Vector3.forward * _nextPosition;
            CurrentLevel = CreateLevel(_levelDataProvider.Data[_index]);
            _nextPosition += _step;
            CurrentLevel.transform.position = position;

            EventBus.Instance.OnNextLevel?.Invoke();
            EventBus.Instance.OnMoveCamera?.Invoke(CurrentLevel);
            EventBus.Instance.OnMovePlayer?.Invoke(CurrentLevel.PlayerPosition.Point);

            EventBus.Instance.OnGetTheme?.Invoke(YandexGame.savesData.CurrentTheme);
        }

        private Level CreateLevel(LevelData data)
        { 
            var level = new GameObject("Level").AddComponent<Level>();
            var spawnPoint = new GameObject("SpawnPoint").AddComponent<PlayerSpawnPoint>();

            for (int i = 0; i < data.Cube.Count; i++)
                Instantiate(_levelBuilderData.Items[0].Item, data.Cube[i], Quaternion.identity, level.transform);
            for (int i = 0; i < data.Cell.Count; i++)
                Instantiate(_levelBuilderData.Items[1].Item, data.Cell[i], Quaternion.identity, level.transform);
            for (int i = 0; i < data.Coin.Count; i++)
                Instantiate(_levelBuilderData.Items[3 ].Item, data.Coin[i], Quaternion.identity, level.transform);

            level.PlayerPosition = spawnPoint;
            spawnPoint.transform.parent = level.transform;
            spawnPoint.SetPosition(data.PlayerSpawnPoint);

            return level;
        }
    }
}