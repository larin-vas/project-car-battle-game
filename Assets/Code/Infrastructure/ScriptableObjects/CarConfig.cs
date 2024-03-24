using Code.Transport.Car;
using Code.Wheel;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{

    [CreateAssetMenu(fileName = "New Car", menuName = "Car")]
    public class CarConfig : ScriptableObject
    {
        public CarView CarView => _carView;

        public float CollisionDamage => _collisionDamage;

        public float MaxHealthPoints => _maxHealthPoints;

        public float MaxAcceleration => _maxAcceleration;

        public float AccelerationRate => _accelerationRate;

        public Vector2 MassCenterPosition => _massCenterPosition;

        public float Mass => _mass;

        public IReadOnlyList<WheelConfig> Wheels => _whees;

        public IReadOnlyList<GunConfig> Guns => _guns;


        [SerializeField]
        private CarView _carView;

        [SerializeField, Min(0)]
        private float _collisionDamage;

        [SerializeField, Min(1f)]
        private float _maxHealthPoints;

        [SerializeField, Min(0)]
        private float _maxAcceleration;

        [SerializeField, Min(0)]
        private float _accelerationRate;

        [SerializeField]
        private Vector2 _massCenterPosition;

        [SerializeField, Min(0)]
        private float _mass;

        [SerializeField]
        private List<WheelConfig> _whees;

        [SerializeField]
        private List<GunConfig> _guns;
    }
}