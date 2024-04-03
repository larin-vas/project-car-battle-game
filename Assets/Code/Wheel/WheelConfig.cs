using System;
using UnityEngine;

namespace Code.Wheel
{
    [Serializable]
    public class WheelConfig
    {
        [field: SerializeField]
        public WheelView WheelView { get; private set; }

        [field: SerializeField]
        public Vector2 LocalPosition { get; private set; }

        [field: SerializeField]
        public WheelAlignment Alignment { get; private set; }

        [field: SerializeField, Range(0f, 360f)]
        public float MinRotationAngle { get; private set; }

        [field: SerializeField, Range(0f, 360f)]
        public float MaxRotationAngle { get; private set; }

        [field: SerializeField, Min(0f)]
        public float RotationSpeed { get; private set; }

        [field: SerializeField]
        public bool IsDriveWheel { get; private set; }

        [field: SerializeField]
        public bool IsLockedOnHandbrake { get; private set; }
    }
}