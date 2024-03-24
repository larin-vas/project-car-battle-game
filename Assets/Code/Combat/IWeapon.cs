using Code.Common.Interfaces;
using Zenject;

namespace Code.Combat
{
    public interface IWeapon : IMovable, IActivatable, ITickable
    {
        public void SetInput(IAimingInput input);
    }
}
