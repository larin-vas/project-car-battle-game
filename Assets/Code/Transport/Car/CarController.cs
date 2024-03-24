using Code.Car;
using Code.Combat;
using Code.Physics;
using Code.Physics.Force;
using Code.Wheel;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Transport.Car
{
    public class CarController : ControllableTransport
    {
        private IMovableInput _input;

        private readonly CarModel _carModel;
        private readonly ITransportView _carView;

        private readonly IWheelSystem _suspension;
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

            _carModel = playerModel;
            _carView = playerView;

            _suspension = suspension;
            _weaponSystem = weaponSystem;

            _collisionTrigger = collisionTrigger;
        }

        public override void Enable()
        {
            _carView.Activator.Enable();
            _suspension.Enable();
        }

        public override void Disable()
        {
            _carView.Activator.Disable();
            _suspension.Disable();
        }

        public override void SetPosition(Vector2 position)
        {
            _carModel.Transformation.Position.Value = position;
            _suspension.SetPosition(position);
        }

        public override void SetRotation(Quaternion rotation)
        {
            _carModel.Transformation.Rotation.Value = rotation;
            _suspension.SetRotation(rotation);
        }

        public override Vector2 GetPosition()
        {
            return _carModel.Transformation.Position.Value;
        }

        public override Quaternion GetRotation()
        {
            return _carModel.Transformation.Rotation.Value;
        }

        public override void SetInput(IInput input)
        {
            _input = input;
            _suspension.SetInput(input);
            _weaponSystem.SetInput(input);
        }

        public override void Tick()
        {
            _suspension.Tick();

            _weaponSystem.Tick();

            UpdateAcceleration();

            UpdateCurrentMassCenter();

            Vector2 rotationCenter = _suspension.CalculateRotationCenter();

            CalculateForcesAndTorques(rotationCenter, out Vector2 totalForce, out float totalTorque);

            totalForce = totalForce.normalized * Mathf.Abs(totalForce.magnitude - totalForce.magnitude * totalForce.magnitude / 5000f);

            UpdatePositionByForce(totalForce);

            UpdateRotationByTorque(rotationCenter, totalTorque);

            UpdateAccelerationVector(totalForce);

            _carModel.InertiaVector.Value = totalForce;

            _weaponSystem.SetPosition(GetPosition());
            _weaponSystem.SetRotation(GetRotation());

            //Debug.Log(_carModel.CurrentHealthPoints);
        }

        private void UpdateAcceleration()
        {
            float newAcceleration = _carModel.CurrentAcceleration;

            if (_input.Movement != 0)
                newAcceleration += _carModel.AccelerationRate * Time.deltaTime;
            else
                newAcceleration -= _carModel.AccelerationRate * 10f * Time.deltaTime;

            newAcceleration = Mathf.Clamp(newAcceleration, 0f, _carModel.MaxAcceleration);

            // Сбрасываем текущее ускорение, если нажата кнопка тормоза или ручного тормоза
            if (_input.Brake || _input.Handbrake)
                newAcceleration = 0f;

            _carModel.CurrentAcceleration = newAcceleration;
        }

        private void UpdateCurrentMassCenter()
        {
            _carModel.CurrentMassCenter =
                GetPosition() +
                (Vector2)(GetRotation() * _carModel.BaseMassCenter) -
                _carModel.AccelerationVector;
        }

        private void UpdateRotationByTorque(Vector2 rotationCenter, float torque)
        {
            float angularVelocity = torque * Time.deltaTime * 40f;

            Quaternion newRotation = GetRotation() * Quaternion.Euler(0f, 0f, angularVelocity);

            RotateAroundPoint(rotationCenter, newRotation);
        }

        private void UpdateAccelerationVector(Vector2 totalForce)
        {
            Vector2 newAccelerationVector = totalForce - _carModel.InertiaVector.Value;
            _carModel.AccelerationVector = Vector2.Lerp(_carModel.AccelerationVector, newAccelerationVector.normalized * Mathf.Clamp(newAccelerationVector.magnitude, 0f, 0.3f), Time.deltaTime);
        }

        private void CalculateForcesAndTorques(Vector2 rotationCenter, out Vector2 totalForce, out float totalTorque)
        {
            IReadOnlyList<CollisionInfo> collisions = _collisionTrigger.GetCollisions();

            totalForce = Vector2.zero;
            totalTorque = 0f;

            foreach (PhysicForce force in _suspension.GetWheelsForces())
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
                float newHealthPoints = _carModel.CurrentHealthPoints - force.CollisionDamage;
                _carModel.CurrentHealthPoints = Mathf.Clamp(newHealthPoints, 0f, _carModel.MaxHealthPoints);
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