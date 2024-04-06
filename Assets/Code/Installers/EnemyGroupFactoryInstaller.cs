using Code.Infrastructure.ScriptableObjects;
using UnityEngine;
using Zenject;
using Code.Infrastructure.Factories;

namespace Code.Installers
{
    public class EnemyGroupFactoryInstaller : MonoInstaller
    {
        [SerializeField]
        private EnemyGroupConfig _enemyGroupConfig;

        public override void InstallBindings()
        {
            InstallConfig();

            InstallFactory();
        }

        private void InstallConfig()
        {
            Container.BindInterfacesAndSelfTo<EnemyGroupConfig>().FromInstance(_enemyGroupConfig).AsSingle();
        }

        private void InstallFactory()
        {
            Container.BindInterfacesAndSelfTo<EnemyGroupFactory>().AsSingle();
        }
    }
}