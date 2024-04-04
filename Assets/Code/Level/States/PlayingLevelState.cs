using Code.Infrastructure.StateMachine;
using Code.AI;
using Zenject;
using Code.GameCamera;
using Code.Map;
using Code.Transport.Car;

namespace Code.Level.States
{
    public class PlayingLevelState : IState, ITickable
    {
        private readonly StateMachine _stateMachine;

        private readonly IUIInput _input;

        private readonly MapController _map;

        private readonly CarController _playerCar;

        private readonly CameraController _camera;

        private readonly EnemyGroupController _enemyGroup;

        private int _enemiesCount;

        public PlayingLevelState(
            StateMachine stateMachine,
            IUIInput input,
            MapController map,
            CarController playerCar,
            EnemyGroupController enemyGroup,
            CameraController camera,
            int enemiesCount)
        {
            _stateMachine = stateMachine;
            _input = input;
            _map = map;
            _playerCar = playerCar;
            _enemyGroup = enemyGroup;
            _camera = camera;
            _enemiesCount = enemiesCount;
        }

        public void Enter()
        {
            _enemyGroup.OnEnemyDie += OnEnemyDie;
        }

        public void Exit()
        {
            _enemyGroup.OnEnemyDie -= OnEnemyDie;
        }

        public void Tick()
        {
            if (_input.PauseButtonPressed)
                _stateMachine.EnterIn<PauseLevelState>();

            _map.Tick();

            _playerCar.Tick();
            _camera.Tick();

            _enemyGroup.Tick();
        }

        private void OnEnemyDie()
        {
            _enemiesCount--;

            if (_enemiesCount <= 0)
                _stateMachine.EnterIn<InitializeLevelState>();
            else
                _enemyGroup.SpawnEnemyAtRandomPosition();
        }
    }
}