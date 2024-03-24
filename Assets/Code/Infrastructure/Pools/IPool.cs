using System.Collections.Generic;

namespace Code.Infrastructure.Pools
{
    public interface IPool<T>
    {
        public T GetFromPool();

        public void ReturnToPool(T obj);

        public void ReturnAllToPool();

        public IEnumerable<T> GetAllActiveObjects();
    }
}