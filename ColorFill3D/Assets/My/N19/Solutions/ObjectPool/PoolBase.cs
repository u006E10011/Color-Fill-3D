using System;
using System.Collections.Generic;
using UnityEngine;

namespace N19
{
    public class PoolBase<T>
    {
        private readonly Func<T> _preload;
        private readonly Action<T> _getItem;
        private readonly Action<T> _returnItem;

        public Queue<T> Pool { get; private set; } = new();
        private List<T> _activeItem = new();

        public PoolBase(Func<T> preload, Action<T> getItem, Action<T> returnItem, int preloadCount)
        {
            _preload = preload;
            _getItem = getItem;
            _returnItem = returnItem;

            if (preload == null)
            {
                Debug.LogError("Preload Func is null");
                return;
            }

            for (int i = 0; i < preloadCount; i++)
                Return(_preload());
        }

        public T Get()
        {
            T item = Pool.Count > 0 ? Pool.Dequeue() : _preload();

            _getItem(item);
            _activeItem.Add(item);

            return item;
        }

        public void Return(T item)
        {
            _returnItem(item);
            Pool.Enqueue(item);
            _activeItem.Remove(item);
        }

        public void ReturnAll()
        {
            foreach (T item in _activeItem.ToArray())
                Return(item);
        }

        public void Set(Queue<T> queue)
        {
            Pool = queue;
        }
    }
}