using Code.Common;
using System;
using UnityEngine;

namespace Code.Combat.Gun
{
    public class GunModel
    {
        public Transformation Transformation { get; }

        public Vector2 LocalPosition { get; }

        public Quaternion LocalRotation { get; set; }

        public float RotationSpeed { get; }

        public float ReloadTime { get; }

        public float ReloadTimeRemaining
        {
            get => _reloadTimeRemaining;
            set
            {
                if (value >= 0f && value <= ReloadTime)
                    _reloadTimeRemaining = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        private float _reloadTimeRemaining;

        public GunModel(
            Transformation transformation, Vector2 localPosition,
            float rotationSpeed, float reloadTime) :
            this(transformation, localPosition, rotationSpeed, reloadTime, 0f)
        { }

        public GunModel(
            Transformation transformation, Vector2 localPosition, 
            float rotationSpeed, float reloadTime, float reloadTimeRemaining)
        {
            if (reloadTime < 0f)
                throw new ArgumentOutOfRangeException(nameof(reloadTime));

            if (rotationSpeed < 0f)
                throw new ArgumentOutOfRangeException(nameof(rotationSpeed));

            Transformation = transformation;

            LocalPosition = localPosition;

            LocalRotation = Quaternion.identity;

            RotationSpeed = rotationSpeed;

            ReloadTime = reloadTime;

            ReloadTimeRemaining = reloadTimeRemaining;
        }
    }
}
