using Zenject;
using Code.Infrastructure.Factories;
using Code.Map;
using Code.Infrastructure.ScriptableObjects;
using UnityEngine;

namespace Code.Installers
{
    public class MapInstaller : MonoInstaller
    {
        [SerializeField]
        private MapConfig _mapConfig;

        public override void InstallBindings()
        {
            InstallMapConfig();

            InstallTileModelFactory();

            InstallMapController();
        }

        private void InstallMapConfig()
        {
            Container.BindInterfacesAndSelfTo<MapConfig>().FromInstance(_mapConfig).AsSingle();
        }

        private void InstallTileModelFactory()
        {
            Container.BindInterfacesAndSelfTo<TileModelFactory>().AsSingle();
        }

        private void InstallMapController()
        {
            Container.Bind<MapController>().FromFactory<MapFactory>().AsSingle();
            Container.Bind<IWorldProperties>().To<MapController>().FromResolve();
        }
    }
}