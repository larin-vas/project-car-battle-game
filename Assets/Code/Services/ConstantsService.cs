using Code.Infrastructure.ScriptableObjects;
using System;
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

        public float MinWheelSlidingCoefficient { get; }

        public float MaxWheelSlidingCoefficient { get; }

        public ConstantsService(ConstantsConfig config)
        {
            if (config.MinWheelSlidingCoefficient > config.MaxWheelSlidingCoefficient)
                throw new ArgumentOutOfRangeException(nameof(config.MinWheelSlidingCoefficient));

            ProjectilePoolInitSize = config.ProjectilePoolInitSize;
            AngleToChangeDirection = config.AngleToChangeDirection;
            MaxMapSeed = config.MaxMapSeed;
            CollisionTriggerBufferSize = config.CollisionTriggerBufferSize;
            PathfinderAllowedDirections = config.PathfinderAllowedDirections;
            MinWheelSlidingCoefficient = config.MinWheelSlidingCoefficient;
            MaxWheelSlidingCoefficient = config.MaxWheelSlidingCoefficient;
        }
    }
}