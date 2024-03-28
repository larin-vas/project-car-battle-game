using Code.AI.Pathfinding;
using Code.Car;
using Code.Infrastructure.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.AI
{
    public class EnemyGroupController : ITickable
    {
        private readonly EnemyGroupModel _model;

        private readonly AIMovementController _movementController;

        private readonly IPool<ControllableTransport> _enemiesPool;

        public event Action OnEnemyDie;

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
            transport.RestoreHealth();

            _model.CurrentEnemies++;
        }

        public void Tick()
        {
            IEnumerable<ControllableTransport> activeEnemies = _enemiesPool.GetAllActiveObjects().ToList();

            foreach (ControllableTransport transport in activeEnemies)
            {
                _movementController.UpdateObjectInput(transport);

                transport.Tick();

                if (Mathf.Approximately(transport.GetCurrentHealth(), 0f))
                {
                    _enemiesPool.ReturnToPool(transport);
                    _model.CurrentEnemies--;
                    OnEnemyDie.Invoke();
                }
            }
        }

        private Vector2 GetRandomSpawnPoint()
        {
            int index = UnityEngine.Random.Range(0, _model.SpawnPoints.Count);
            return _model.SpawnPoints[index];
        }
    }
}