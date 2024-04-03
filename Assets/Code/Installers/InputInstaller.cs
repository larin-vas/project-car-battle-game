using Code.Infrastructure.ScriptableObjects;
using UnityEngine.InputSystem;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerInputConfig _inputConfig;

        [SerializeField]
        private PlayerInput _playerInput;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputConfig>().FromInstance(_inputConfig).AsSingle();

            Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle();
        }
    }
}