using Code.Physics;
using Code.Services;
using Zenject;

public class BaseServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PhysicsService>().AsSingle();

        Container.BindInterfacesAndSelfTo<ConstantsService>().AsSingle();
    }
}
