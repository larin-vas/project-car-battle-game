using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.AI
{
    public class EnemyGroupModel
    {
        public int MaxEnemies { get; }

        public int CurrentEnemies
        {
            get => _currentEnemies;
            set
            {
                if (value >= 0f && value <= MaxEnemies)
                    _currentEnemies = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        public float AttackRange { get; }

        public int PathSegmentIndex { get; }

        public IReadOnlyList<Vector2> SpawnPoints { get; }

        private int _currentEnemies;

        public EnemyGroupModel(int maxEnemies, float attackRange, int pathSegmentIndex, IReadOnlyList<Vector2> spawnPoints)
        {
            if (pathSegmentIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(pathSegmentIndex));

            if (attackRange < 0)
                throw new ArgumentOutOfRangeException(nameof(attackRange));

            MaxEnemies = maxEnemies;
            CurrentEnemies = MaxEnemies;

            AttackRange = attackRange;

            PathSegmentIndex = pathSegmentIndex;

            SpawnPoints = spawnPoints;
        }
    }
}