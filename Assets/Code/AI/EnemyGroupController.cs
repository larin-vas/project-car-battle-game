using Code.Car;
using Code.Infrastructure.Pools;
using UnityEngine;
using Zenject;

namespace Code.AI
{
    public class EnemyGroupController : ITickable
    {
        private readonly EnemyGroupModel _model;

        private readonly AIMovementController _movementController;

        private readonly IPool<ControllableTransport> _enemiesPool;

        public EnemyGroupController(
            EnemyGroupModel model,
            AIMovementController movementController,
            IPool<ControllableTransport> enemiesPool)
        {
            _model = model;
            _movementController = movementController;
            _enemiesPool = enemiesPool;
        }

        public void Initialize()
        {
            _model.CurrentEnemies = 0;
            _enemiesPool.ReturnAllToPool();

            for (int i = 0; i < _model.MaxEnemies; i++)
            {
                SpawnEnemyAtRandomPosition();
            }
        }

        public void SpawnEnemyAtRandomPosition()
        {
            Vector2 randomPosition = GetRandomSpawnPoint();

            SpawnEnemy(randomPosition);
        }

        public void SpawnEnemy(Vector2 position)
        {
            if (_model.CurrentEnemies > _model.MaxEnemies)
                return;

            ControllableTransport transport = _enemiesPool.GetFromPool();

            transport.SetPosition(position);

            _model.CurrentEnemies++;
        }

        public void Tick()
        {
            foreach (ControllableTransport transport in _enemiesPool.GetAllActiveObjects())
            {
                _movementController.UpdateObjectInput(transport);

                transport.Tick();
            }
        }

        private Vector2 GetRandomSpawnPoint()
        {
            int index = Random.Range(0, _model.SpawnPoints.Count);
            return _model.SpawnPoints[index];
        }
    }
}