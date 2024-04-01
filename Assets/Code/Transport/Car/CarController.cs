using Assets.Code.Transport.Car.CarHealth;
using Assets.Code.Transport.Car.CarMovement;
using Code.Car;
using Code.Combat;
using Code.Physics.Force;
using Code.Wheel;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Transport.Car
{
    public class CarController : ControllableTransport
    {
        private readonly CarMovementController _movementController;
        private readonly CarHealthController _healthController;

        private readonly IWheelSystem _wheelSystem;
        private readonly IWeaponSystem _weaponSystem;

        public CarController(
            CarMovementController movementController,
            CarHealthController healthController,
            IWheelSystem wheelSystem,
            IWeaponSystem weaponSystem)
        {
            _movementController = movementController;
            _healthController = healthController;
            _wheelSystem = wheelSystem;
            _weaponSystem = weaponSystem;
        }

        public override void Enable()
        {
            _movementController.Enable();
            _healthController.Enable();
            _weaponSystem.Enable();
            _wheelSystem.Enable();
        }

        public override void Disable()
        {
            _movementController.Disable();
            _healthController.Disable();
            _weaponSystem.Disable();
            _wheelSystem.Disable();
        }

        public override void SetPosition(Vector2 position)
        {
            _movementController.SetPosition(position);
            _wheelSystem.SetPosition(position);
            _weaponSystem.SetPosition(position);
        }

        public override void SetRotation(Quaternion rotation)
        {
            _movementController.SetRotation(rotation);
            _wheelSystem.SetRotation(rotation);
            _weaponSystem.SetRotation(rotation);
        }

        public override Vector2 GetPosition()
        {
            return _movementController.GetPosition();
        }

        public override Quaternion GetRotation()
        {
            return _movementController.GetRotation();
        }

        public override float GetCurrentHealth()
        {
            return _healthController.GetCurrentHealth();
        }

        public override void RestoreHealth()
        {
            _healthController.RestoreHealth();
        }

        public override void SetInput(IInput input)
        {
            _movementController.SetInput(input);
            _wheelSystem.SetInput(input);
            _weaponSystem.SetInput(input);
        }

        public override void Tick()
        {
            // Update the wheel system
            _wheelSystem.Tick();

            // Update movement controller
            Vector2 rotationCenter = _wheelSystem.CalculateRotationCenter();
            IEnumerable<PhysicForce> wheelForces = _wheelSystem.GetWheelsForces();

            _movementController.SetAppliedForces(wheelForces);
            _movementController.SetRotationCenter(rotationCenter);

            _movementController.Tick();

            // Update weapon system
            _weaponSystem.SetPosition(GetPosition());
            _weaponSystem.SetRotation(GetRotation());

            _weaponSystem.Tick();

            // Update health controller
            _healthController.Tick();
        }
    }
}