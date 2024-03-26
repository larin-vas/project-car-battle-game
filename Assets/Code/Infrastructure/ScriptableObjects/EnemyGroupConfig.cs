using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy Group Config", menuName = "Enemy Group Config")]
    public class EnemyGroupConfig : ScriptableObject
    {
        public int MaxEnemies => _maxEnemies;

        public float AttackRange => _attackRange;

        public int PathSegmentIndexAsTarget => _pathSegmentIndexAsTarget;

        public IReadOnlyList<Vector2> SpawnPoints => _spawnPoints;

        [SerializeField, Min(0)]
        private int _maxEnemies;

        [SerializeField, Min(0f)]
        private float _attackRange;

        [SerializeField, Min(0)]
        private int _pathSegmentIndexAsTarget;

        [SerializeField]
        private List<Vector2> _spawnPoints;
    }
}