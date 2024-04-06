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
            InstallPhysicsConfig();

            InstallConstantsConfig();
        }

        private void InstallPhysicsConfig()
        {
            Container.BindInterfacesAndSelfTo<PhysicsConfig>().FromInstance(_physicsConfig).AsSingle();
        }

        private void InstallConstantsConfig()
        {
            Container.BindInterfacesAndSelfTo<ConstantsConfig>().FromInstance(_constantsConfig).AsSingle();
        }
    }
}