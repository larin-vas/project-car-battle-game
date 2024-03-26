using Code.Car;
using Code.Infrastructure.Pools;
using UnityEngine;
using Zenject;

namespace Code.AI
{
    public class AIController : ITickable
    {
        private readonly AIMovementController _movementController;

        private readonly IPool<ControllableTransport> _pool;

        private readonly float _enemyCount;

        public AIController(
            AIMovementController movementController,
            IPool<ControllableTransport> pool,
            int enemyCount)
        {
            _movementController = movementController;
            _pool = pool;
            _enemyCount = enemyCount;
        }

        public void Initialize()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                ControllableTransport transport = _pool.GetFromPool();

                transport.SetPosition(new Vector2(Random.Range(10f, 100f), Random.Range(10f, 100f)));
            }
        }

        public void Tick()
        {
            foreach (ControllableTransport transport in _pool.GetAllActiveObjects())
            {
                _movementController.UpdateObjectInput(transport);
                transport.Tick();
            }
        }
    }
}