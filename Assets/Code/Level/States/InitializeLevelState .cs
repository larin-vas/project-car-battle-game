using Assets.Code.Infrastructure.StateMachine;
using UnityEngine;
using Code.GameCamera;
using Code.Map;
using Code.Transport.Car;

namespace Code.Level.States
{
    public class InitializeLevelState : IState
    {
        private readonly StateMachine _stateMachine;

        private readonly MapController _map;

        private readonly CarController _playerCar;

        private readonly CarController _enemyCar;

        private CameraController _camera;

        public InitializeLevelState(
            StateMachine stateMachine,
            MapController map,
            CarController playerCar,
            CarController enemyCar,
            CameraController camera)
        {
            _stateMachine = stateMachine;
            _map = map;
            _playerCar = playerCar;
            _enemyCar = enemyCar;
            _camera = camera;
        }

        public void Enter()
        {
            _map.Generate();

            _playerCar.SetPosition(new Vector2(50, 50));

            _camera.SetPosition(_playerCar.GetPosition());

            _enemyCar.SetPosition(new Vector2(30, 30));

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