using Code.Combat.Projectile;
using Code.Common;
using Code.Physics;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class ProjectileFactory : IFactory<ProjectileConfig, ProjectileController>
    {
        private readonly ConstantsService _constants;

        private readonly Collider2D _ignoredCollider;

        public ProjectileFactory(ConstantsService constants, Collider2D ignoredCollider)
        {
            _constants = constants;
            _ignoredCollider = ignoredCollider;
        }

        public ProjectileController Create(ProjectileConfig config)
        {
            if (config == null)
                throw new System.NullReferenceException();

            ProjectileView view = Object.Instantiate(config.ProjectileView);

            Transformation transformation = new Transformation();

            Observable<Vector2> directionVector = new Observable<Vector2>(Vector2.zero);

            Observable<float> damage = new Observable<float>(config.Damage);

            ProjectileModel model = new ProjectileModel(transformation, directionVector, damage, config.Speed);

            view.PhysicUpdater.Construct(transformation, directionVector, damage, new Observable<float>(0f), _ignoredCollider);

            ContactFilter2D contactFilter = new ContactFilter2D();
            int layerToIgnore = Physics2D.GetLayerCollisionMask(view.gameObject.layer);
            contactFilter.SetLayerMask(layerToIgnore);
            contactFilter.useLayerMask = true;

            CollisionTrigger collisionTrigger = new CollisionTrigger(_constants, view.Collider, contactFilter, _ignoredCollider);

            ProjectileController Projectile = new ProjectileController(model, view, collisionTrigger);

            return Projectile;
        }
    }
}