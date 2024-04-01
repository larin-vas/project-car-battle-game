using UnityEngine;
using Code.Common;
using System;

namespace Assets.Code.Transport.Car.CarMovement
{
    public class CarMovementModel
    {
        public Transformation Transformation { get; }

        public Observable<Vector2> InertiaVector { get; set; }

        public Observable<float> CollisionDamage { get; }

        // Acceleration
        public Vector2 AccelerationVector { get; set; }
        public float CurrentAcceleration
        {
            get => _currentAcceleration;
            set
            {
                if (value >= 0 && value <= MaxAcceleration)
                    _currentAcceleration = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
        public float MaxAcceleration { get; }
        public float AccelerationRate { get; }

        // Mass
        public Vector2 CurrentMassCenter { get; set; }
        public Vector2 BaseMassCenter { get; }
        public float Mass { get; }

        public Vector2 CurrentRotationCenter { get; set; }

        private float _currentAcceleration;

        public CarMovementModel(
            Transformation transformation,
            Observable<Vector2> inertiaVector,
            Observable<float> collisionDamage,
            float accelerationRate, float maxAcceleration,
            Vector2 massCenter, float mass)
        {
            Transformation = transformation;

            InertiaVector = inertiaVector;

            CollisionDamage = collisionDamage;

            AccelerationRate = accelerationRate;
            MaxAcceleration = maxAcceleration;

            CurrentMassCenter = massCenter;
            BaseMassCenter = massCenter;
            Mass = mass;

            CurrentRotationCenter = Vector2.zero;
        }
    }
}