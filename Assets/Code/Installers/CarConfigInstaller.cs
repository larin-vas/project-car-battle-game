using Code.Infrastructure.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class CarConfigInstaller : MonoInstaller
    {
        [SerializeField]
        private CarConfig _carConfig;

        [SerializeField]
        private string _configID;

        public override void InstallBindings()
        {
            InstallConfig();
        }

        private void InstallConfig()
        {
            Container.Bind<CarConfig>().WithId(_configID).FromInstance(_carConfig);
        }
    }
}