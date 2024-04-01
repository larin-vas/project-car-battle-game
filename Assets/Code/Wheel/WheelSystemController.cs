using Assets.Code.Transport.Car.CarMovement;
using Code.Physics.Force;
using Code.Transport;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Wheel
{
    public class WheelSystemController : IWheelSystem
    {
        private IMovableInput _input;

        private readonly CarMovementModel _transportModel;

        private readonly ICollection<WheelController> _wheels;

        public WheelSystemController(
            IMovableInput input,
            CarMovementModel carModel,
            ICollection<WheelController> wheels)
        {
            _input = input;
            _transportModel = carModel;
            _wheels = wheels;
        }

        public void Enable()
        {
            foreach (WheelController wheel in _wheels)
            {
                wheel.Enable();
            }
        }

        public void Disable()
        {
            foreach (WheelController wheel in _wheels)
            {
                wheel.Disable();
            }
        }

        public void SetPosition(Vector2 position)
        {
            foreach (WheelController wheel in _wheels)
            {
                wheel.SetPosition(position);
            }
        }

        public void SetRotation(Quaternion rotation)
        {
            foreach (WheelController wheel in _wheels)
            {
                wheel.SetRotation(rotation);
            }
        }

        public IEnumerable<PhysicForce> GetWheelsForces()
        {
            foreach (WheelController wheel in _wheels)
                yield return new PhysicForce(wheel.GetPosition(), GetWheelForceDirection(wheel));
        }

        public void SetInput(IMovableInput input)
        {
            _input = input;
        }

        public Vector2 CalculateRotationCenter()
        {
            Vector2 rotationCenter = Vector2.zero;
            int wheelsCount = 0;
            foreach (WheelController wheel in _wheels)
            {
                if (wheel.IsSteerableWheel())
                {
                    if (_input.Rotation < 0 && wheel.GetAlignment() == WheelAlignment.Left)
                    {
                        rotationCenter += wheel.GetPosition();
                        wheelsCount++;
                    }
                    else if (_input.Rotation > 0 && wheel.GetAlignment() == WheelAlignment.Right)
                    {
                        rotationCenter += wheel.GetPosition();
                        wheelsCount++;
                    }
                    else if (_input.Rotation == 0)
                    {
                        rotationCenter += wheel.GetPosition();
                        wheelsCount++;
                    }
                }
            }
            return rotationCenter / wheelsCount;
        }

        public void Tick()
        {
            foreach (WheelController wheel in _wheels)
            {
                wheel.Tick();
            }
        }

        private float CalculateWheelSlidingCoefficient(WheelController wheel)
        {
            float baseDistance = (wheel.GetPosition() - CalculateCarCenter()).magnitude;
            float currentDistance = (wheel.GetPosition() - _transportModel.CurrentMassCenter).magnitude;
            float Coefficient = 1f / (1f + Mathf.Exp(-_transportModel.Mass * (currentDistance - baseDistance)));
            return 0.05f + Coefficient * 0.795f;
        }

        private Vector2 CalculateCarCenter()
        {
            Vector2 carCenter = Vector2.zero;
            foreach (WheelController wheel in _wheels)
            {
                carCenter += wheel.GetPosition();
            }
            return carCenter / _wheels.Count;
        }

        private Vector2 GetWheelForceDirection(WheelController wheel)
        {
            float handbrakeCoefficient = (wheel.IsLockedOnHandbrake() && _input.Handbrake) ? 0f : 1f;
            float brakeCoefficient = _input.Brake ? 0f : 1f;
            float driveWheelCoefficient = wheel.IsDriveWheel() ? _transportModel.CurrentAcceleration * _input.Movement * Time.deltaTime : 0f;

            Vector2 wheelDirection =
                wheel.GetRotation() * Vector3.up *
                brakeCoefficient *
                handbrakeCoefficient;

            Vector2 forceDirection =
                wheelDirection *
                driveWheelCoefficient +

                wheelDirection *
                _transportModel.InertiaVector.Value.magnitude *
                CalculateInertiaReductionCoefficient(wheel) *
                CalculateInertiaSeparationCoefficient() +

                _transportModel.InertiaVector.Value *
                CalculateWheelSlidingCoefficient(wheel) *
                CalculateInertiaSeparationCoefficient();

            return forceDirection;
        }

        private float CalculateInertiaReductionCoefficient(WheelController wheel)
        {
            Vector3 rotationInertiaToWheel = Quaternion.FromToRotation(_transportModel.InertiaVector.Value, wheel.GetRotation() * Vector3.up).eulerAngles;
            float angle = (rotationInertiaToWheel.y != 0) ? rotationInertiaToWheel.y : rotationInertiaToWheel.z;
            return Mathf.Cos(angle * Mathf.Deg2Rad);
        }

        private float CalculateInertiaSeparationCoefficient()
        {
            float sum = 0f;
            foreach (WheelController wheel in _wheels)
            {
                float inertiaReductionCoefficient = CalculateInertiaReductionCoefficient(wheel);
                float brakeCoefficient = (_input.Brake) ? 0f : 1f;
                float handbrakeCoefficient = (wheel.IsLockedOnHandbrake() && _input.Handbrake) ? 0f : 1f;
                sum +=
                    inertiaReductionCoefficient * inertiaReductionCoefficient * brakeCoefficient * handbrakeCoefficient +
                    CalculateWheelSlidingCoefficient(wheel);
            }
            return 1f / sum;
        }
    }
}
