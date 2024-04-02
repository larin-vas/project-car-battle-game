using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{

    [CreateAssetMenu(fileName = "New Physics Config", menuName = "Physics Config")]
    public class PhysicsConfig : ScriptableObject
    {
        [field: SerializeField, Min(0f)]
        public float RotationSpeed { get; private set; }

        [field: SerializeField, Min(0f)]
        public float MaxMassCenterDeviation { get; private set; }

        [field: SerializeField, Min(0f)]
        public float AccelerationDecayRate { get; private set; }
    }
}