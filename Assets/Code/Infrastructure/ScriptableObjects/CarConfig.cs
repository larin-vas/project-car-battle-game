using Code.Transport.Car;
using Code.Wheel;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Car", menuName = "Configs/Car")]
    public class CarConfig : ScriptableObject
    {
        [field: SerializeField]
        public CarView CarView { get; private set; }

        [field: SerializeField, Min(0f)]
        public float CollisionDamage { get; private set; }

        [field: SerializeField, Min(0f)]
        public float MaxHealthPoints { get; private set; }

        [field: SerializeField, Min(0f)]
        public float MaxAcceleration { get; private set; }

        [field: SerializeField, Min(0f)]
        public float AccelerationRate { get; private set; }

        [field: SerializeField]
        public Vector2 MassCenterPosition { get; private set; }

        [field: SerializeField, Min(0f)]
        public float Mass { get; private set; }

        [field: SerializeField]
        public List<WheelConfig> Wheels { get; private set; }

        [field: SerializeField]
        public List<GunConfig> Guns { get; private set; }
    }
}