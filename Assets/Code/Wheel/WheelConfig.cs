using System;
using UnityEngine;

namespace Code.Wheel
{
    [Serializable]
    public class WheelConfig
    {
        public WheelView WheelView => _wheelView;

        public Vector2 LocalPosition => _localPosition;

        public WheelAlignment Alignment => _alignment;

        public float MinRotationAngle => _minRotationAngle;
        public float MaxRotationAngle => _maxRotationAngle;

        public float RotationSpeed => _rotationSpeed;

        public bool IsDriveWheel => _isDriveWheel;

        public bool IsLockedOnHandbrake => _isLockedOnHandbrake;


        [SerializeField]
        private WheelView _wheelView;

        [SerializeField]
        private Vector2 _localPosition;

        [SerializeField]
        private WheelAlignment _alignment;

        [SerializeField, Range(0, 360)]
        private float _minRotationAngle;
        [SerializeField, Range(0, 360)]
        private float _maxRotationAngle;

        [SerializeField, Min(0)]
        private float _rotationSpeed;

        [SerializeField]
        private bool _isDriveWheel;

        [SerializeField]
        private bool _isLockedOnHandbrake;
    }
}