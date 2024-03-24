using System;
using UnityEngine;

namespace Code.Physics
{
    public class CollisionInfo
    {
        public Vector2 Point { get; }

        public Vector2 ObjectForceVector { get; }

        public float CollisionDamage { get; }

        public CollisionInfo(Vector2 point, Vector2 objectForceVector, float collisionDamage)
        {
            if (collisionDamage < 0)
                throw new ArgumentOutOfRangeException(nameof(collisionDamage));

            Point = point;
            ObjectForceVector = objectForceVector;
            CollisionDamage = collisionDamage;
        }
    }
}
