using Code.Car;
using Code.Combat;
using Code.Physics;
using Code.Physics.Force;
using Code.Wheel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Transport.Car
{
    public class CarController : ControllableTransport
    {
        private IMovableInput _input;

        private readonly CarModel _model;
        private readonly ITransportView _view;

        private readonly IWheelSystem _wheelSystem;
        private readonly IWeaponSystem _weaponSystem;

        private readonly ICollisionTrigger _collisionTrigger;

        public CarController(
            IMovableInput input,
            CarModel playerModel, ITransportView playerView,
            IWheelSystem suspension,
            IWeaponSystem weaponSystem,
            ICollisionTrigger collisionTrigger)
        {
            _input = input;

            _model = playerModel;
            _view = playerView;

            _wheelSystem = suspension;
            _weaponSystem = weaponSystem;

            _collisionTrigger = collisionTrigger;
        }

        public override void Enable()
        {
            _view.Activator.Enable();
            _weaponSystem.Enable();
            _wheelSystem.Enable();
        }

        public override void Disable()
        {
            _view.Activator.Disable();
            _weaponSystem.Disable();
            _wheelSystem.Disable();
        }

        public override void SetPosition(Vector2 position)
        {
            _model.Transformation.Position.Value = position;
            _wheelSystem.SetPosition(position);
            _weaponSystem.SetPosition(position);
        }

        public override void SetRotation(Quaternion rotation)
        {
            _model.Transformation.Rotation.Value = rotation;
            _wheelSystem.SetRotation(rotation);
            _weaponSystem.SetRotation(rotation);
        }

        public override Vector2 GetPosition()
        {
            return _model.Transformation.Position.Value;
        }

        public override Quaternion GetRotation()
        {
            return _model.Transformation.Rotation.Value;
        }

        public override float GetCurrentHealth()
        {
            return _model.CurrentHealthPoints;
        }

        public override void RestoreHealth()
        {
            _model.CurrentHealthPoints = _model.MaxHealthPoints;
        }

        public override void SetInput(IInput input)
        {
            _input = input;
            _wheelSystem.SetInput(input);
            _weaponSystem.SetInput(input);
        }

        public override void Tick()
        {
            _wheelSystem.Tick();

            _weaponSystem.Tick();

            UpdateAcceleration();

            UpdateCurrentMassCenter();

            Vector2 rotationCenter = _wheelSystem.CalculateRotationCenter();

            CalculateForcesAndTorques(rotationCenter, out Vector2 totalForce, out float totalTorque);

            totalForce = totalForce.normalized * Mathf.Abs(totalForce.magnitude - totalForce.magnitude * totalForce.magnitude / 5000f);

            UpdatePositionByForce(totalForce);

            UpdateRotationByTorque(rotationCenter, totalTorque);

            UpdateAccelerationVector(totalForce);

            _model.InertiaVector.Value = totalForce;

            _weaponSystem.SetPosition(GetPosition());
            _weaponSystem.SetRotation(GetRotation());
        }

        private void UpdateAcceleration()
        {
            float newAcceleration = _model.CurrentAcceleration;

            if (_input.Movement != 0)
                newAcceleration += _model.AccelerationRate * Time.deltaTime;
            else
                newAcceleration -= _model.AccelerationRate * 10f * Time.deltaTime;

            newAcceleration = Mathf.Clamp(newAcceleration, 0f, _model.MaxAcceleration);

            // Сбрасываем текущее ускорение, если нажата кнопка тормоза или ручного тормоза
            if (_input.Brake || _input.Handbrake)
                newAcceleration = 0f;

            _model.CurrentAcceleration = newAcceleration;
        }

        private void UpdateCurrentMassCenter()
        {
            _model.CurrentMassCenter =
                GetPosition() +
                (Vector2)(GetRotation() * _model.BaseMassCenter) -
                _model.AccelerationVector;
        }

        private void UpdateRotationByTorque(Vector2 rotationCenter, float torque)
        {
            float angularVelocity = torque * Time.deltaTime * 40f;

            Quaternion newRotation = GetRotation() * Quaternion.Euler(0f, 0f, angularVelocity);

            RotateAroundPoint(rotationCenter, newRotation);
        }

        private void UpdateAccelerationVector(Vector2 totalForce)
        {
            Vector2 newAccelerationVector = totalForce - _model.InertiaVector.Value;
            _model.AccelerationVector =
                Vector2.Lerp(
                    _model.AccelerationVector,
                    newAccelerationVector.normalized * Mathf.Clamp(newAccelerationVector.magnitude, 0f, 0.3f),
                    Time.deltaTime);
        }

        private void CalculateForcesAndTorques(Vector2 rotationCenter, out Vector2 totalForce, out float totalTorque)
        {
            IReadOnlyList<CollisionInfo> collisions = _collisionTrigger.GetCollisions();

            totalForce = Vector2.zero;
            totalTorque = 0f;

            foreach (PhysicForce force in _wheelSystem.GetWheelsForces())
            {
                totalForce += force.Direction;

                // Рассчитываем момент вращения как кросс-продукт силы и расстояния до точки приложения силы
                Vector2 distance = force.Point - rotationCenter;
                totalTorque += Vector3.Cross(distance, force.Direction).z;
            }

            foreach (CollisionInfo force in collisions)
            {
                // Рассчитываем момент вращения как кросс-продукт силы и расстояния до точки приложения силы
                Vector2 distance = force.Point - rotationCenter;
                totalTorque += Vector3.Cross(distance, -totalForce).z;
                float newHealthPoints = _model.CurrentHealthPoints - force.CollisionDamage;
                _model.CurrentHealthPoints = Mathf.Clamp(newHealthPoints, 0f, _model.MaxHealthPoints);
            }
            if ((collisions.Count > 0) &&
                ((collisions[0].Point - (GetPosition() - totalForce * Time.deltaTime / 10f)).magnitude > (collisions[0].Point - (GetPosition() + totalForce * Time.deltaTime)).magnitude))
                totalForce = -totalForce / 10f;
        }

        private void RotateAroundPoint(Vector2 rotationCenter, Quaternion newRotation)
        {
            // Calculate the current offset from the rotation center
            Vector2 currentOffset = GetPosition() - rotationCenter;

            // Save the angle before setting the new rotation
            float baseAngle = GetRotation().eulerAngles.z;

            // Set the new rotation
            SetRotation(newRotation);

            // Get the angle after setting the new rotation
            float newAngle = GetRotation().eulerAngles.z;

            // Calculate the cos and sin of the angle difference for optimization
            float angleDifference = (newAngle - baseAngle) * Mathf.Deg2Rad;
            float cosAngle = Mathf.Cos(angleDifference);
            float sinAngle = Mathf.Sin(angleDifference);

            // Apply the rotation to the current offset
            Vector2 newOffset = new Vector2(
                currentOffset.x * cosAngle - currentOffset.y * sinAngle,
                currentOffset.x * sinAngle + currentOffset.y * cosAngle);

            // Set the new position
            SetPosition(rotationCenter + newOffset);
        }
    }
}