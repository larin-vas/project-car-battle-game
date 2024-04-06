using Code.Infrastructure.ScriptableObjects;
using UnityEngine.InputSystem;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerInputConfig _inputConfig;

        [SerializeField]
        private PlayerInput _playerInput;

        public override void InstallBindings()
        {
            InstallConfig();

            InstallUnityPlayerInput();

            InstallPlayerInput();
        }

        private void InstallConfig()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputConfig>().FromInstance(_inputConfig).AsSingle();
        }

        private void InstallUnityPlayerInput()
        {
            Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle();
        }

        private void InstallPlayerInput()
        {
            Container.BindInterfacesAndSelfTo<MainPlayerInput>().AsSingle();
        }
    }
}