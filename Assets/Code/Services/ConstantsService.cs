using Code.Infrastructure.ScriptableObjects;
using UnityEngine;

namespace Code.Services
{
    public class ConstantsService
    {
        public int ProjectilePoolInitSize { get; }

        public float AngleToChangeDirection { get; }

        public int MaxMapSeed { get; }

        public int CollisionTriggerBufferSize { get; }

        public Vector2Int[] PathfinderAllowedDirections { get; }

        public ConstantsService(ConstantsConfig config)
        {
            ProjectilePoolInitSize = config.ProjectilePoolInitSize;
            AngleToChangeDirection = config.AngleToChangeDirection;
            MaxMapSeed = config.MaxMapSeed;
            CollisionTriggerBufferSize = config.CollisionTriggerBufferSize;
            PathfinderAllowedDirections = config.PathfinderAllowedDirections;
        }
    }
}