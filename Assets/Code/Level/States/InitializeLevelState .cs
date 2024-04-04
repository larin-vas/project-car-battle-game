using Code.Infrastructure.StateMachine;
using UnityEngine;
using Code.GameCamera;
using Code.Map;
using Code.Transport.Car;
using Code.AI;

namespace Code.Level.States
{
    public class InitializeLevelState : IState
    {
        private readonly StateMachine _stateMachine;

        private readonly MapController _map;

        private readonly CarController _playerCar;

        private readonly EnemyGroupController _enemyGroup;

        private CameraController _camera;

        private readonly Vector2 _playerStartPosition;

        public InitializeLevelState(
            StateMachine stateMachine,
            MapController map,
            CarController playerCar,
            EnemyGroupController enemyGroup,
            CameraController camera,
            Vector2 playerStartPosition)
        {
            _stateMachine = stateMachine;
            _map = map;
            _playerCar = playerCar;
            _enemyGroup = enemyGroup;
            _camera = camera;
            _playerStartPosition = playerStartPosition;
        }

        public void Enter()
        {
            _map.Generate();

            _playerCar.SetPosition(_playerStartPosition);

            _camera.SetPosition(_playerCar.GetPosition());

            _enemyGroup.Initialize();

            _stateMachine.EnterIn<PlayingLevelState>();
        }

        public void Exit()
        { }

        public void Tick()
        {
            Debug.Log(_map.GetGenerationProgress());
        }
    }
}