using UnityEngine;

namespace Code.Physics
{
    public interface IPhysicObject
    {
        public Vector2 Force { get; }

        public float CollisionDamage { get; }

        public float ElasticityRate { get; }
    }
}
