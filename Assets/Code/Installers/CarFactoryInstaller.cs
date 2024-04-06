using Code.Infrastructure.Factories;
using Zenject;

namespace Code.Installers
{
    public class CarFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallWheelFactory();

            InstallGunFactory();

            InstallCarFactory();
        }

        private void InstallWheelFactory()
        {
            Container.BindInterfacesAndSelfTo<WheelFactory>().AsSingle();
        }

        private void InstallGunFactory()
        {
            Container.BindInterfacesAndSelfTo<GunFactory>().AsSingle();
        }

        private void InstallCarFactory()
        {
            Container.BindInterfacesAndSelfTo<CarFactory>().AsSingle();
        }
    }
}