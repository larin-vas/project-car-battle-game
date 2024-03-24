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

        public bool Handbrake
        {
            get => _handbrake;
            set => _handbrake = value;
        }

        public bool Brake
        {
            get => _brake;
            set => _brake = value;
        }

        public Vector2 Direction { get; set; }

        public bool Shoot { get; set; }

        private float _movement;
        private float _rotation;
        private bool _handbrake;
        private bool _brake;

        public AIMovableInput() : this(0f, 0f, false, false)
        { }

        public AIMovableInput(float movement, float rotation, bool handbrake, bool brake)
        {
            SetValues(movement, rotation, handbrake, brake);
        }

        public void SetValues(float movement, float rotation, bool handbrake, bool brake)
        {
            Movement = movement;
            Rotation = rotation;
            Handbrake = handbrake;
            Brake = brake;
        }
    }
}