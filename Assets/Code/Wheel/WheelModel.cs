using Code.Common;
using System;
using UnityEngine;

namespace Code.Wheel
{
    public class WheelModel
    {
        public Transformation Transformation { get; }

        public WheelAlignment Alignment { get; }

        public float MinRotationAngle { get; }
        public float MaxRotationAngle { get; }

        public float RotationSpeed { get; }

        public Vector2 LocalPosition { get; }

        public bool IsLockedOnHandbrake { get; }

        public bool IsDriveWheel { get; }

        public Quaternion LocalRotation
        {
            get => _localRotation;
            set
            {
                if (IsRotationUnderMax(value))
                    _localRotation = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        private Quaternion _localRotation;

        public WheelModel(Transformation transformation, Vector2 localPosition) :
            this(transformation, localPosition, false, false)
        { }

        public WheelModel(Transformation transformation, Vector2 localPosition, bool isLockedOnHandbrake, bool isDriveWheel) :
            this(transformation, localPosition, WheelAlignment.Center, 0f, 0f, 0f, isLockedOnHandbrake, isDriveWheel)
        { }

        public WheelModel(Transformation transformation, Vector2 localPosition, WheelAlignment alignment,
            float minRotationAngle, float maxRotationAngle, float rotationSpeed,
            bool isDriveWheel, bool isLockedOnHandbrake)
        {
            if (rotationSpeed < 0)
                throw new ArgumentOutOfRangeException(nameof(rotationSpeed));

            Transformation = transformation;

            Alignment = alignment;

            MinRotationAngle = minRotationAngle;
            MaxRotationAngle = maxRotationAngle;
            RotationSpeed = rotationSpeed;

            LocalPosition = localPosition;

            LocalRotation = Quaternion.identity;

            IsDriveWheel = isDriveWheel;

            IsLockedOnHandbrake = isLockedOnHandbrake;
        }

        private bool IsRotationUnderMax(Quaternion rotation)
        {
            return (Mathf.Abs(rotation.eulerAngles.z) <= MaxRotationAngle) ||
                   (360f - Mathf.Abs(rotation.eulerAngles.z) <= MaxRotationAngle);
        }
    }
}
