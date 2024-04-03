using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{

    [CreateAssetMenu(fileName = "New Level Config", menuName = "Configs/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField]
        public Vector2 PlayerStartPosition { get; private set; }

        [field: SerializeField, Min(0)]
        public int TargetEnemiesCount { get; private set; }
    }
}