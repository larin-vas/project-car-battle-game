using Zenject;

namespace Assets.Code.Infrastructure.StateMachine
{
    public interface IState : ITickable
    {
        public void Enter();
        public void Exit();
    }
}