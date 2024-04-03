using Code.Infrastructure.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class BaseConfigurationsInstaller : MonoInstaller
    {
        [SerializeField]
        private PhysicsConfig _physicsConfig;

        [SerializeField]
        private ConstantsConfig _constantsConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PhysicsConfig>().FromInstance(_physicsConfig).AsSingle();

            Container.BindInterfacesAndSelfTo<ConstantsConfig>().FromInstance(_constantsConfig).AsSingle();
        }

    }
}