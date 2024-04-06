using Code.Physics;
using Code.Services;
using Zenject;

namespace Code.Installers
{
    public class BaseServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallPhysicsService();

            InstallConstantsService();
        }

        private void InstallPhysicsService()
        {
            Container.BindInterfacesAndSelfTo<PhysicsService>().AsSingle();
        }

        private void InstallConstantsService()
        {
            Container.BindInterfacesAndSelfTo<ConstantsService>().AsSingle();
        }
    }
}