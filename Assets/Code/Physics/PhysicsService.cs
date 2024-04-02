using Code.Infrastructure.ScriptableObjects;

namespace Code.Physics
{
    public class PhysicsService
    {
        public float RotationSpeed { get; }

        public float MaxMassCenterDeviation { get; }

        public float AccelerationDecayRate { get; }

        public PhysicsService(PhysicsConfig config)
        {
            RotationSpeed = config.RotationSpeed;
            MaxMassCenterDeviation = config.MaxMassCenterDeviation;
            AccelerationDecayRate = config.AccelerationDecayRate;
        }
    }
}
