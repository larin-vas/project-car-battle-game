using Code.Common.Interfaces;
using System.Collections.Generic;
using Zenject;

namespace Code.Infrastructure.Pools
{
    public class ObjectPool<TConfig, T> : IPool<T> where T : IActivatable
    {
        private readonly IFactory<TConfig, T> _factory;

        private readonly TConfig _config;

        private readonly Stack<T> _pool;

        private readonly HashSet<T> _activeObjects;

        public ObjectPool(IFactory<TConfig, T> factory, TConfig config, int initialSize)
        {
            _factory = factory;

            _config = config;

            _pool = new Stack<T>(initialSize);

            _activeObjects = new HashSet<T>();

            InitializePool(initialSize);
        }

        public T GetFromPool()
        {
            if (_pool.Count > 0)
            {
                T obj = _pool.Pop();
                obj.Enable();
                _activeObjects.Add(obj);

                return obj;
            }
            else
            {
                T obj = _factory.Create(_config);
                _activeObjects.Add(obj);

                return obj;
            }
        }

        public void ReturnToPool(T obj)
        {
            obj.Disable();
            _pool.Push(obj);
            _activeObjects.Remove(obj);
        }

        public void ReturnAllToPool()
        {
            foreach (T obj in _activeObjects)
            {
                obj.Disable();
                _pool.Push(obj);
            }
            _activeObjects.Clear();
        }

        public IEnumerable<T> GetAllActiveObjects()
        {
            return _activeObjects;
        }

        private void InitializePool(int initialSize)
        {
            for (int i = 0; i < initialSize; i++)
            {
                T obj = _factory.Create(_config);
                obj.Disable();
                _pool.Push(obj);
            }
        }
    }
}