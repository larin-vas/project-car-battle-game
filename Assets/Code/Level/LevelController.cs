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
        private readonly IFactory<IReadOnlyMovable, CameraController> _cameraFactory;
        private readonly IFactory<IReadOnlyMovable, EnemyGroupController> _enemyGroupFactory;

        private readonly CarConfig _playerCarConfig;

        public LevelController(
            IUIInput input,
            MapController map,
            IFactory<CarConfig, CarController> carFactory,
            IFactory<IReadOnlyMovable, CameraController> cameraFactory,
            IFactory<IReadOnlyMovable, EnemyGroupController> enemyGroupFactory,
            [Inject(Id = "Player")] CarConfig playerCarConfig)
        {
            _stateMachine = new StateMachine();

            _input = input;

            _map = map;

            _carFactory = carFactory;
            _cameraFactory = cameraFactory;
            _enemyGroupFactory = enemyGroupFactory;

            _playerCarConfig = playerCarConfig;
        }

        public void Start()
        {
            CarController playerCar = _carFactory.Create(_playerCarConfig);

            CameraController camera = _cameraFactory.Create(playerCar);

            EnemyGroupController enemyGroup = _enemyGroupFactory.Create(playerCar);

            InitializeStates(playerCar, enemyGroup, camera);

            _stateMachine.EnterIn<InitializeLevelState>();
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }

        public void InitializeStates(CarController playerCar, EnemyGroupController enemyGroup, CameraController camera)
        {
            InitializeLevelState initializeLevelState = new InitializeLevelState(_stateMachine, _map, playerCar, enemyGroup, camera);

            PlayingLevelState playingLevelState = new PlayingLevelState(_stateMachine, _input, _map, playerCar, enemyGroup, camera);

            PauseLevelState pauseLevelState = new PauseLevelState(_stateMachine, _input);

            _stateMachine.AddStates(initializeLevelState, playingLevelState, pauseLevelState);
        }
    }
}