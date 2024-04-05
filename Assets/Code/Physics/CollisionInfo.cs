using System;
using UnityEngine;

namespace Code.Physics
{
    public class CollisionInfo
    {
        public Vector2 Point { get; }

        public Vector2 ObjectForceVector { get; }

        public float CollisionDamage { get; }

        public float ElasticityRate { get; }

        public CollisionInfo(
            Vector2 point, Vector2 objectForceVector,
            float collisionDamage, float elasticityRate)
        {
            if (collisionDamage < 0)
                throw new ArgumentOutOfRangeException(nameof(collisionDamage));

            if (elasticityRate < 0 || elasticityRate > 1f)
                throw new ArgumentOutOfRangeException(nameof(elasticityRate));

            Point = point;
            ObjectForceVector = objectForceVector;
            CollisionDamage = collisionDamage;
            ElasticityRate = elasticityRate;
        }
    }
}
