using Code.Infrastructure.ScriptableObjects;
using Code.Level;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private LevelConfig _levelConfig;

        public override void InstallBindings()
        {
            InstallConfig();

            InstallController();
        }

        private void InstallConfig()
        {
            Container.BindInterfacesAndSelfTo<LevelConfig>().FromInstance(_levelConfig).AsSingle();
        }

        private void InstallController()
        {
            Container.BindInterfacesAndSelfTo<LevelController>().AsSingle();
        }
    }
}