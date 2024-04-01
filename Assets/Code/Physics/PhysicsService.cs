using System;
using UnityEngine;

namespace Code.Physics
{
    public class PhysicsService
    {
        public float RotationSpeed { get; }

        public float MaxMassCenterDeviation { get; }

        public float AccelerationDecayRate { get; }

        public PhysicsService()
        {
        }
    }
}
