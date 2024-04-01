using Code.Physics.Force;
using Code.Physics;
using Code.Transport.Car;
using System.Collections.Generic;
using UnityEngine;
using Code.Common.AbstractClasses;
using Code.Common.Interfaces;
using Zenject;

namespace Assets.Code.Transport.Car.CarMovement
{
    public class CarMovementController : ForceMovable, IActivatable, ITickable
    {
        private IMovableInput _input;

        private readonly PhysicsService _physics;

        private readonly CarMovementModel _model;
        private readonly ITransportView _view;

        private readonly ICollisionTrigger _collisionTrigger;

        private IEnumerable<PhysicForce> _appliedForces;

        public CarMovementController(
            IMovableInput input,
            PhysicsService physics,
            CarMovementModel model,
            ITransportView view,
            ICollisionTrigger collisionTrigger)
        {
            _input = input;

            _physics = physics;

            _model = model;
            _view = view;

            _collisionTrigger = collisionTrigger;

            _appliedForces = new List<PhysicForce>();
        }

        public void Enable()
        {
            _view.Activator.Enable();
        }

        public void Disable()
        {
            _view.Activator.Disable();
        }

        public override void SetPosition(Vector2 position)
        {
            _model.Transformation.Position.Value = position;
        }

        public override void SetRotation(Quaternion rotation)
        {
            _model.Transformation.Rotation.Value = rotation;
        }

        public override Vector2 GetPosition()
        {
            return _model.Transformation.Position.Value;
        }

        public override Quaternion GetRotation()
        {
            return _model.Transformation.Rotation.Value;
        }

        public void SetInput(IInput input)
        {
            _input = input;
        }

        public void SetAppliedForces(IEnumerable<PhysicForce> appliedForces)
        {
            _appliedForces = appliedForces;
        }

        public void SetRotationCenter(Vector2 rotationCenter)
        {
            _model.CurrentRotationCenter = rotationCenter;
        }

        public void Tick()
        {
            UpdateAcceleration();

            UpdateCurrentMassCenter();

            Vector2 totalForce = CalculateTotalForce();

            float totalTorque = CalculateTorque(totalForce);

            UpdatePositionByForce(totalForce);

            UpdateRotationByTorque(totalTorque);

            UpdateAccelerationVector(totalForce);

            _model.InertiaVector.Value = totalForce;
        }

        private void UpdateAcceleration()
        {
            float newAcceleration = _model.CurrentAcceleration;

            if (_input.Movement != 0)
                newAcceleration += _model.AccelerationRate * Time.deltaTime;
            else
                newAcceleration -= _model.AccelerationRate * _physics.AccelerationDecayRate * Time.deltaTime;

            newAcceleration = Mathf.Clamp(newAcceleration, 0f, _model.MaxAcceleration);

            // Сбрасываем текущее ускорение, если нажата кнопка тормоза или ручного тормоза
            if (_input.Brake || _input.Handbrake)
                newAcceleration = 0f;

            _model.CurrentAcceleration = newAcceleration;
        }

        private void UpdateCurrentMassCenter()
        {
            Vector2 rotatedBaseMassCenter = GetRotation() * _model.BaseMassCenter;

            _model.CurrentMassCenter =
                GetPosition() + rotatedBaseMassCenter - _model.AccelerationVector;
        }

        private void UpdateRotationByTorque(float torque)
        {
            float angularVelocity = torque * Time.deltaTime * _physics.RotationSpeed;

            Quaternion newRotation = GetRotation() * Quaternion.Euler(0f, 0f, angularVelocity);

            RotateAroundPoint(_model.CurrentRotationCenter, newRotation);
        }

        private void UpdateAccelerationVector(Vector2 totalForce)
        {
            Vector2 newAcceleration = totalForce - _model.InertiaVector.Value;
            Vector2 clampedNewAcceleration = Vector2.ClampMagnitude(newAcceleration, _physics.MaxMassCenterDeviation);

            _model.AccelerationVector =
                Vector2.Lerp(_model.AccelerationVector, clampedNewAcceleration, Time.deltaTime);
        }

        private Vector2 CalculateTotalForce()
        {
            Vector2 totalForce = Vector2.zero;

            foreach (PhysicForce force in _appliedForces)
            {
                totalForce += force.Direction;
            }

            IReadOnlyList<CollisionInfo> collisions = _collisionTrigger.GetCollisions();

            if (collisions.Count > 0)
            {
                float elasticityRate = collisions[0].ElasticityRate;

                Vector2 vectorOnBounce = collisions[0].Point - (GetPosition() - totalForce * elasticityRate * Time.deltaTime);
                Vector2 vectorOnMove = collisions[0].Point - (GetPosition() + totalForce * Time.deltaTime);

                if (vectorOnBounce.magnitude > vectorOnMove.magnitude)
                    totalForce = -totalForce * elasticityRate;
            }

            totalForce = totalForce.normalized * Mathf.Abs(totalForce.magnitude - totalForce.magnitude * totalForce.magnitude / 5000f);

            return totalForce;
        }

        private float CalculateTorque(Vector2 totalForce)
        {
            IReadOnlyList<CollisionInfo> collisions = _collisionTrigger.GetCollisions();

            float totalTorque = 0f;

            foreach (PhysicForce force in _appliedForces)
            {
                // Calculating the torque as the cross product of force and the distance to the point of force application
                Vector2 distance = force.Point - _model.CurrentRotationCenter;
                totalTorque += Vector3.Cross(distance, force.Direction).z;
            }

            foreach (CollisionInfo force in collisions)
            {
                // Calculating the torque as the cross product of force and the distance to the point of force application
                Vector2 distance = force.Point - _model.CurrentRotationCenter;
                totalTorque += Vector3.Cross(distance, -totalForce).z;
            }

            return totalTorque;
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