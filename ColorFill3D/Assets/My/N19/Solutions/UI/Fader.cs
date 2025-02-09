using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace N19
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private int _indexLoadScene;

        [SerializeField, Space(10)] private float _delayStartAnimation;
        [SerializeField] private float _durationAnimation;
        [SerializeField] private float _delayLoadScene;

        [SerializeField, Space(10)] private Image _icon;

        private float _timeElapsed;
        private bool _loading;

        private AsyncOperation _asyncOperation;

        private void Start() => StartCoroutine(LoadScene());

        private void Update() => Invoke(nameof(AnimationProgress), _delayStartAnimation);

        private IEnumerator LoadScene()
        {
            _asyncOperation = SceneManager.LoadSceneAsync(_indexLoadScene);
            _asyncOperation.allowSceneActivation = false;

            while (!_asyncOperation.allowSceneActivation)
            {
                if(_loading)
                {
                    yield return new WaitForSecondsRealtime(_delayLoadScene);
        
                    Load();
                }

                yield return null;
            }
        }
        private void Load() => _asyncOperation.allowSceneActivation = true;
        private void AnimationProgress()
        {
            _timeElapsed += Time.unscaledDeltaTime;
            //_icon.fillAmount = Mathf.Lerp(0, 0.9f, _timeElapsed / _durationAnimation);
            UIExtension.FillAmount(_icon, 0.9f, _timeElapsed / _durationAnimation);

            if (_icon.fillAmount > 0.89f && (_asyncOperation != null && _asyncOperation.progress >= 0.89f))
            {
                _loading = true;
                _icon.fillAmount = 1;
            }
        }
    }
}