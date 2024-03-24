using Code.Common;
using System;
using UnityEngine;

namespace Code.Transport.Car
{
    public class CarModel : ITransportModel
    {
        public Transformation Transformation { get; }

        public Observable<Vector2> InertiaVector { get; set; }

        public Observable<float> CollisionDamage { get; }

        // Health Points
        public float CurrentHealthPoints
        {
            get => _currentHealthPoints;
            set
            {
                if (value >= 0 && value <= MaxHealthPoints)
                    _currentHealthPoints = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
        public float MaxHealthPoints { get; }

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

        private float _currentAcceleration;
        private float _currentHealthPoints;

        public CarModel(
            Transformation transformation,
            Observable<Vector2> inertiaVector,
            Observable<float> collisionDamage,
            float maxHealthPoints,
            float accelerationRate, float maxAcceleration,
            Vector2 massCenter, float mass)
        {
            Transformation = transformation;

            InertiaVector = inertiaVector;

            CollisionDamage = collisionDamage;

            MaxHealthPoints = maxHealthPoints;
            CurrentHealthPoints = maxHealthPoints;

            AccelerationRate = accelerationRate;
            MaxAcceleration = maxAcceleration;

            CurrentMassCenter = massCenter;
            BaseMassCenter = massCenter;
            Mass = mass;
        }
    }
}
