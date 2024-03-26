using Assets.Code.Infrastructure.StateMachine;
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

        private readonly EnemyGroupController _aiController;

        private CameraController _camera;

        public InitializeLevelState(
            StateMachine stateMachine,
            MapController map,
            CarController playerCar,
            EnemyGroupController aiController,
            CameraController camera)
        {
            _stateMachine = stateMachine;
            _map = map;
            _playerCar = playerCar;
            _aiController = aiController;
            _camera = camera;
        }

        public void Enter()
        {
            _map.Generate();

            _playerCar.SetPosition(new Vector2(50, 50));

            _camera.SetPosition(_playerCar.GetPosition());

            _aiController.Initialize();

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