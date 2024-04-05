using Code.Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Physics
{
    public class CollisionTrigger : ICollisionTrigger
    {
        private readonly ConstantsService _constants;

        private readonly Collider2D _collider;

        private readonly ContactFilter2D _filter;

        private readonly Collider2D[] _collidersBuffer;

        private readonly Collider2D[] _ignoredColliders;

        public CollisionTrigger(
            ConstantsService constants,
            Collider2D collider) :
            this(constants, collider, new ContactFilter2D())
        { }

        public CollisionTrigger(
            ConstantsService constants,
            Collider2D collider,
            ContactFilter2D filter) :
            this(constants, collider, filter, null)
        { }

        public CollisionTrigger(
            ConstantsService constants,
            Collider2D collider,
            params Collider2D[] ignoredColliders) :
            this(constants, collider, new ContactFilter2D(), ignoredColliders)
        { }

        public CollisionTrigger(
            ConstantsService constants,
            Collider2D collider,
            ContactFilter2D filter,
            params Collider2D[] ignoredColliders)
        {
            _constants = constants;
            _collider = collider;
            _filter = filter;
            _collidersBuffer = new Collider2D[_constants.CollisionTriggerBufferSize];
            _ignoredColliders = ignoredColliders;
        }

        public IReadOnlyList<CollisionInfo> GetCollisions()
        {
            List<CollisionInfo> collisions = new List<CollisionInfo>();

            int colliderCount = Physics2D.OverlapCollider(_collider, _filter, _collidersBuffer);

            for (int i = 0; i < colliderCount; i++)
            {
                Collider2D collider = _collidersBuffer[i];

                if (_ignoredColliders != null && _ignoredColliders.Contains(collider))
                    continue;

                ColliderDistance2D distance = _collider.Distance(collider);

                if (collider.TryGetComponent(out IPhysicObject transformableObject))
                {
                    CollisionInfo collision = new CollisionInfo(
                        distance.pointA, transformableObject.Force,
                        transformableObject.CollisionDamage, transformableObject.ElasticityRate);

                    collisions.Add(collision);
                }
            }

            return collisions;
        }
    }
}