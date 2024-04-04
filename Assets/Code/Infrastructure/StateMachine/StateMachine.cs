using System;
using System.Collections.Generic;
using Zenject;

namespace Code.Infrastructure.StateMachine
{
    public class StateMachine : ITickable
    {
        private readonly Dictionary<Type, IState> _states;

        private IState _currentState;

        public StateMachine()
        {
            _states = new Dictionary<Type, IState>();
        }

        public StateMachine(params IState[] states)
        {
            _states = new Dictionary<Type, IState>();

            AddStates(states);
        }

        public void EnterIn<TState>() where TState : IState
        {
            if (_states.TryGetValue(typeof(TState), out IState state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
        }

        public void AddStates(params IState[] states)
        {
            foreach (IState state in states)
            {
                AddState(state);
            }
        }

        public void RemoveStates(params IState[] states)
        {
            foreach (IState state in states)
            {
                RemoveState(state);
            }
        }

        public void AddState(IState state)
        {
            Type type = state.GetType();

            if (!HasState(state))
                _states.Add(type, state);
        }

        public void RemoveState(IState state)
        {
            Type type = state.GetType();

            if (HasState(state))
                _states.Remove(type);
        }

        public bool HasState(IState state)
        {
            Type type = state.GetType();

            return _states.ContainsKey(type);
        }

        public void Tick()
        {
            _currentState?.Tick();
        }
    }
}