using Assets.Code.Infrastructure.StateMachine;
using Zenject;
using UnityEngine;

namespace Code.Level.States
{
    public class PauseLevelState : IState, ITickable
    {
        private readonly StateMachine _stateMachine;

        private readonly IUIInput _input;

        public PauseLevelState(StateMachine stateMachine, IUIInput input)
        {
            _stateMachine = stateMachine;
            _input = input;
        }

        public void Enter()
        {
            Debug.Log("PAUSE");
        }

        public void Exit()
        { }

        public void Tick()
        {
            if (_input.PauseButtonPressed)
                _stateMachine.EnterIn<PlayingLevelState>();
        }
    }
}