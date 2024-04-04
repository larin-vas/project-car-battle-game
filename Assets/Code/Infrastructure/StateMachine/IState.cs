using Zenject;

namespace Code.Infrastructure.StateMachine
{
    public interface IState : ITickable
    {
        public void Enter();
        public void Exit();
    }
}