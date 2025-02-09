using System;
using System.Collections;
using UnityEngine;

namespace N19
{
    public class Timer : IService
    {
        /// <param name="target">Длительность</param>
        /// <param name="ascend">
        /// Режим таймера
        /// <br>true - по возрастание</br>
        /// <br>false - по убыванию</br>
        /// </param>
        /// <param name="intervalElapsed">
        /// Интервал, через которое будет вызыватся ивент OnIntervalElapsed
        /// <br>По умолчанию 1 секунда</br>
        /// </param>
        public Timer(MonoBehaviour owner, float target = float.MaxValue, bool ascend = true, bool isUnscaleDeltaTime = false, float intervalElapsed = 1)
        {
            _owner = owner;
            _target = target;
            _intervalElapsed = intervalElapsed;
            _isUnscaleDeltaTime = isUnscaleDeltaTime;
            _ascend = ascend;
        }

        public Action OnStart;
        public Action OnUpdate;
        public Action OnIntervalElapsed;
        public Action OnTimerFinished;

        public float Value { get; private set; }

        private readonly bool _ascend;
        private readonly bool _isUnscaleDeltaTime;
        private readonly float _target;
        private readonly float _intervalElapsed;

        private MonoBehaviour _owner;
        private Coroutine _coroutine;

        public void Start()
        {
            _coroutine = _owner.StartCoroutine(ValueChange());

            OnStart?.Invoke();
        }

        public void Stop()
        {
            if (_coroutine != null)
                _owner.StopCoroutine(_coroutine);

            OnTimerFinished?.Invoke();
        }

        private IEnumerator ValueChange()
        {
            var time = _isUnscaleDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;
            float interval = 0;

            if (_ascend)
            {
                while (Value < _target)
                {
                    Value += time;
                    interval += time;

                    OnUpdate?.Invoke();
                    IntervalElapsed(ref interval);

                    yield return null;
                }
            }
            else
            {
                Value = _target;

                while (Value > 0)
                {
                    Value -= time;
                    interval += time;

                    OnUpdate?.Invoke();
                    IntervalElapsed(ref interval);

                    yield return null;
                }
            }

            Value = Mathf.Clamp(Value, 0, _target);
            Stop();
        }

        private void IntervalElapsed(ref float interval)
        {
            if (interval >= _intervalElapsed)
            {
                interval -= _intervalElapsed;
                OnIntervalElapsed?.Invoke();
            }
        }
    }
}