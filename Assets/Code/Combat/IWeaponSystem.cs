using Code.Common.Interfaces;
using Zenject;

namespace Code.Combat
{
    public interface IWeaponSystem : ISetOnlyMovable, IActivatable, ITickable
    {
        public void SetInput(IAimingInput input);
    }
}
