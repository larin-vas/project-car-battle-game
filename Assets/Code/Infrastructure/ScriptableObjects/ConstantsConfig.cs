using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Constants Config", menuName = "Configs/Constants Config")]
    public class ConstantsConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)]
        public int ProjectilePoolInitSize { get; private set; }

        [field: SerializeField, Range(0f, 180f)]
        public float AngleToChangeDirection { get; private set; }

        [field: SerializeField, Min(0)]
        public int MaxMapSeed { get; private set; }

        [field: SerializeField, Min(0)]
        public int CollisionTriggerBufferSize { get; private set; }

        [field: SerializeField]
        public Vector2Int[] PathfinderAllowedDirections { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float MinWheelSlidingCoefficient { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float MaxWheelSlidingCoefficient { get; private set; }
    }
}