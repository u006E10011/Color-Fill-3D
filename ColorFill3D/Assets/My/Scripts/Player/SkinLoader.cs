using UnityEngine;
using YG;

namespace Project
{
    public class SkinLoader : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private ProductData _data;

        [SerializeField] private float _scale = .8f;

        private GameObject _skin;

        private void OnEnable() => EventBus.Instance.OnGetSkin += CreateSkin;
        private void OnDisable() => EventBus.Instance.OnGetSkin -= CreateSkin;

        private void Start() => CreateSkin(YandexGame.savesData.CurrentThemeSkin);

        private void CreateSkin(int index)
        {
            if(_skin != null)
                Destroy(_skin);

            _skin = Instantiate(_data.Skins[index], transform.position, Quaternion.identity, _player.transform);
            _skin.transform.localScale = Vector3.one * _scale;
        }
    }
}