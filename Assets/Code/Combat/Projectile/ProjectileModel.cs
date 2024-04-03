using Code.Common;
using System;
using UnityEngine;

namespace Code.Combat.Projectile
{
    public class ProjectileModel
    {
        public Transformation Transformation { get; }

        public Observable<Vector2> Force { get; set; }

        public Vector2 Direction { get; set; }

        public Observable<float> Damage { get; }

        public float Speed { get; }

        public ProjectileModel(
            Transformation transformation, 
            Observable<Vector2> force, 
            Observable<float> damage, 
            float speed) :
            this(transformation, force, Vector2.zero, damage, speed)
        { }

        public ProjectileModel(
            Transformation transformation, 
            Observable<Vector2> force, 
            Vector2 direction, 
            Observable<float> damage, 
            float speed)
        {
            if (damage.Value < 0f)
                throw new ArgumentOutOfRangeException(nameof(damage));

            if (speed <= 0f)
                throw new ArgumentOutOfRangeException(nameof(speed));

            Transformation = transformation;

            Direction = direction;

            Force = force;

            Damage = damage;

            Speed = speed;
        }
    }
}
