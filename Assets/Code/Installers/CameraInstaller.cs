using Code.Infrastructure.ScriptableObjects;
using UnityEngine;
using Zenject;
using Code.Infrastructure.Factories;

namespace Code.Installers
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraConfig _cameraConfig;

        public override void InstallBindings()
        {
            InstallConfig();

            InstallFactory();
        }

        private void InstallConfig()
        {
            Container.BindInterfacesAndSelfTo<CameraConfig>().FromInstance(_cameraConfig).AsSingle();
        }

        private void InstallFactory()
        {
            Container.BindInterfacesAndSelfTo<CameraFactory>().AsSingle();
        }
    }
}