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
    public class AIControllerFactory : IFactory<IReadOnlyMovable, int, AIController>
    {
        private readonly IPathfinder _pathfinder;

        private readonly IFactory<CarConfig, CarController> _carFactory;

        private readonly CarConfig _enemyCarConfig;
        public AIControllerFactory(
            IPathfinder pathfinder, 
            IFactory<CarConfig, CarController> carFactory, 
            [Inject(Id = "Enemy")] CarConfig enemyCarConfig)
        {
            _pathfinder = pathfinder;
            _carFactory = carFactory;
            _enemyCarConfig = enemyCarConfig;
        }

        public AIController Create(IReadOnlyMovable targetObject, int enemyCount)
        {
            if (targetObject == null)
                throw new System.NullReferenceException();

            if(enemyCount<0) 
                throw new System.ArgumentOutOfRangeException();

            AIMovementController movementController = new AIMovementController(_pathfinder, targetObject);

            IPool<ControllableTransport> pool = new ObjectPool<CarConfig, ControllableTransport>(_carFactory, _enemyCarConfig, enemyCount);

            AIController aiController = new AIController(movementController, pool, enemyCount);

            return aiController;
        }
    }
}