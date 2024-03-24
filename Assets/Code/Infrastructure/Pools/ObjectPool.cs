using Code.Common.Interfaces;
using System.Collections.Generic;
using Zenject;

namespace Code.Infrastructure.Pools
{
    public class ObjectPool<T> : IPool<T> where T : IActivatable
    {
        private readonly IFactory<T> _factory;

        private readonly Stack<T> _pool;

        private readonly HashSet<T> _activeObjects;

        public ObjectPool(IFactory<T> factory, int initialSize)
        {
            _factory = factory;

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
                T obj = _factory.Create();
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
                T obj = _factory.Create();
                obj.Disable();
                _pool.Push(obj);
            }
        }
    }
}