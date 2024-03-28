using Code.Common.AbstractClasses;
using Code.Common.Interfaces;
using Zenject;

namespace Code.Car
{
    public abstract class ControllableTransport : ForceMovable, IActivatable, IHealthProvider, ITickable
    {
        public abstract void Enable();

        public abstract void Disable();

        public abstract float GetCurrentHealth();

        public abstract void RestoreHealth();

        public abstract void SetInput(IInput input);

        public abstract void Tick();

    }
}