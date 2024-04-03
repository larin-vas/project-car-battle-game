using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy Group Config", menuName = "Configs/Enemy Group Config")]
    public class EnemyGroupConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)]
        public int MaxEnemies { get; private set; }

        [field: SerializeField, Min(0f)]
        public float AttackRange { get; private set; }

        [field: SerializeField, Min(0)]
        public int PathSegmentIndexAsTarget { get; private set; }

        [field: SerializeField]
        public List<Vector2> SpawnPoints { get; private set; }
    }
}