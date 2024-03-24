using System;
using UnityEngine;

namespace Code.AI
{
    public class AIMovableInput : IInput
    {
        public float Movement
        {
            get => _movement;
            set
            {
                if (value <= 1f && value >= -1f)
                    _movement = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        public float Rotation
        {
            get => _rotation;
            set
            {
                if (value <= 1f && value >= -1f)
                    _rotation = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        public bool Handbrake { get; set; }

        public bool Brake { get; set; }


        public Vector2 AimDirection { get; set; }

        public bool Shoot { get; set; }


        private float _movement;

        private float _rotation;

        public AIMovableInput() : this(0f, 0f, false, false, Vector2.zero, false)
        { }

        public AIMovableInput(float movement, float rotation, bool handbrake, bool brake, Vector2 aimDirection, bool shoot)
        {
            SetMovableInput(movement, rotation, handbrake, brake);
            SetAimingInput(aimDirection, shoot);
        }

        public void SetMovableInput(float movement, float rotation, bool handbrake, bool brake)
        {
            Movement = movement;
            Rotation = rotation;
            Handbrake = handbrake;
            Brake = brake;
        }

        public void SetAimingInput(Vector2 aimDirection, bool shoot)
        {
            AimDirection = aimDirection;
            Shoot = shoot;
        }
    }
}