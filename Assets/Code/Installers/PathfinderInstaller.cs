using Zenject;
using Code.AI.Pathfinding;

namespace Code.Installers
{
    public class PathfinderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallPathfinder();
        }

        private void InstallPathfinder()
        {
            Container.BindInterfacesAndSelfTo<AStarPathfinder>().AsSingle();
        }
    }
}