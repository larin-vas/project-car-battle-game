using Assets.Code.Infrastructure.StateMachine;
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

        private readonly EnemyGroupController _ai;

        public PlayingLevelState(
            StateMachine stateMachine,
            IUIInput input,
            MapController map,
            CarController playerCar,
            EnemyGroupController ai,
            CameraController camera)
        {
            _stateMachine = stateMachine;
            _input = input;
            _map = map;
            _playerCar = playerCar;
            _ai = ai;
            _camera = camera;
        }

        public void Enter()
        { }

        public void Exit()
        { }

        public void Tick()
        {
            if (_input.PauseButtonPressed)
                _stateMachine.EnterIn<PauseLevelState>();

            _map.Tick();

            _playerCar.Tick();
            _camera.Tick();

            _ai.Tick();
        }
    }
}