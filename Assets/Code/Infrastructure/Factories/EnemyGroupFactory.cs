using Code.AI;
using Code.AI.Pathfinding;
using Code.Car;
using Code.Common.Interfaces;
using Code.Infrastructure.Pools;
using Code.Infrastructure.ScriptableObjects;
using Code.Transport.Car;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class EnemyGroupFactory : IFactory<IReadOnlyMovable, EnemyGroupController>
    {
        private readonly IPathfinder _pathfinder;

        private readonly IFactory<CarConfig, CarController> _carFactory;

        private readonly EnemyGroupConfig _enemyGroupConfig;

        private readonly CarConfig _enemyCarConfig;
        public EnemyGroupFactory(
            IPathfinder pathfinder,
            IFactory<CarConfig, CarController> carFactory,
            EnemyGroupConfig enemyGroupConfig,
            [Inject(Id = "Enemy")] CarConfig enemyCarConfig)
        {
            _pathfinder = pathfinder;
            _carFactory = carFactory;
            _enemyGroupConfig = enemyGroupConfig;
            _enemyCarConfig = enemyCarConfig;
        }

        public EnemyGroupController Create(IReadOnlyMovable targetObject)
        {
            if (targetObject == null)
                throw new System.NullReferenceException();

            EnemyGroupModel enemyGroupModel = new EnemyGroupModel(
                _enemyGroupConfig.MaxEnemies,
                _enemyGroupConfig.AttackRange,
                _enemyGroupConfig.PathSegmentIndexAsTarget,
                _enemyGroupConfig.SpawnPoints);

            AIMovementController movementController =
                new AIMovementController(enemyGroupModel, _pathfinder, targetObject);

            IPool<ControllableTransport> pool =
                new ObjectPool<CarConfig, ControllableTransport>(_carFactory, _enemyCarConfig, _enemyGroupConfig.MaxEnemies);

            EnemyGroupController aiController =
                new EnemyGroupController(enemyGroupModel, movementController, pool);

            return aiController;
        }
    }
}