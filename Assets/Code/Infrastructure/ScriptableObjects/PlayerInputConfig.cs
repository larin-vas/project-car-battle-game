using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Input Config", menuName = "Player Input Config")]
    public class PlayerInputConfig : ScriptableObject
    {
        [field: SerializeField, Header("Transport Controls")]
        public string MovementActionName { get; private set; }

        [field: SerializeField]
        public string RotationActionName { get; private set; }

        [field: SerializeField]
        public string HandbrakeActionName { get; private set; }

        [field: SerializeField]
        public string BrakeActionName { get; private set; }


        [field: SerializeField, Header("Weapon Controls")]
        public string AimTargetPointActionName { get; private set; }

        [field: SerializeField]
        public string ShootActionName { get; private set; }


        [field: SerializeField, Header("UI Controls")]
        public string PauseButtonActionName { get; private set; }
    }
}