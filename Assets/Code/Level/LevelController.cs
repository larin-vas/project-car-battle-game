using Code.AI.Pathfinding;
using Code.AI;
using Code.GameCamera;
using Code.Map;
using Code.Transport.Car;
using Code.Infrastructure.ScriptableObjects;
using Zenject;
using Code.Level.States;
using Assets.Code.Infrastructure.StateMachine;
using Code.Common.Interfaces;

namespace Code.Level
{
    public class LevelController : ITickable
    {
        private readonly StateMachine _stateMachine;

        private readonly IUIInput _input;

        private readonly MapController _map;

        private readonly IFactory<CarConfig, CarController> _carFactory;
        private readonly IFactory<IMovable, CameraController> _cameraFactory;

        private readonly CarConfig _playerCarConfig;
        private readonly CarConfig _enemyCarConfig;

        private readonly IPathfinder _pathfinder;

        public LevelController(
            IUIInput input,
            MapController map,
            IFactory<CarConfig, CarController> carFactory,
            IFactory<IMovable, CameraController> cameraFactory,
            [Inject(Id = "Player")] CarConfig playerCarConfig,
            [Inject(Id = "Enemy")] CarConfig enemyCarConfig,
            IPathfinder pathfinder)
        {
            _stateMachine = new StateMachine();

            _input = input;

            _map = map;
            
            _carFactory = carFactory;
            _cameraFactory = cameraFactory;

            _playerCarConfig = playerCarConfig;
            _enemyCarConfig = enemyCarConfig;

            _pathfinder = pathfinder;
        }

        public void Start()
        {
            CarController playerCar = _carFactory.Create(_playerCarConfig);

            CameraController camera = _cameraFactory.Create(playerCar);

            CarController enemyCar = _carFactory.Create(_enemyCarConfig);

            AIController ai = new AIController(_pathfinder, enemyCar, playerCar);

            InitializeStates(playerCar, enemyCar, ai, camera);

            _stateMachine.EnterIn<InitializeLevelState>();
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }

        public void InitializeStates(CarController playerCar, CarController enemyCar, AIController ai, CameraController camera)
        {
            InitializeLevelState initializeLevelState = new InitializeLevelState(_stateMachine, _map, playerCar, enemyCar, camera);

            PlayingLevelState playingLevelState = new PlayingLevelState(_stateMachine, _input, _map, playerCar, ai, camera);

            PauseLevelState pauseLevelState = new PauseLevelState(_stateMachine, _input);

            _stateMachine.AddStates(initializeLevelState, playingLevelState, pauseLevelState);
        }
    }
}