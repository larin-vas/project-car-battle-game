using Code.Common.AbstractClasses;
using Code.Common.Interfaces;
using Zenject;

namespace Code.Car
{
    public abstract class ControllableTransport : ForceMovable, IActivatable, ITickable
    {
        public abstract void Disable();

        public abstract void Enable();

        public abstract void SetInput(IInput input);

        public abstract void Tick();
    }
}