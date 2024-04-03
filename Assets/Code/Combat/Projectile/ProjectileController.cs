using Code.Common.AbstractClasses;
using Code.Common.Interfaces;
using Code.Physics;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Combat.Projectile
{
    public class ProjectileController : ForceMovable, IActivatable, ITickable
    {
        public bool HasHit { get; private set; }

        private readonly ProjectileModel _model;
        private readonly ProjectileView _view;

        private readonly ICollisionTrigger _collisionTrigger;

        public ProjectileController(
            ProjectileModel model, ProjectileView view,
            ICollisionTrigger collisionTrigger)
        {
            _model = model;
            _view = view;

            _collisionTrigger = collisionTrigger;

            HasHit = false;
        }

        public void Enable()
        {
            _view.Activator.Enable();
            HasHit = false;
        }

        public void Disable()
        {
            _view.Activator.Disable();
        }

        public override Vector2 GetPosition()
        {
            return _model.Transformation.Position.Value;
        }

        public override Quaternion GetRotation()
        {
            return _model.Transformation.Rotation.Value;
        }

        public override void SetPosition(Vector2 position)
        {
            _model.Transformation.Position.Value = position;
        }

        public override void SetRotation(Quaternion rotation)
        {
            _model.Transformation.Rotation.Value = rotation;
        }

        public void SetDirection(Vector2 direction)
        {
            _model.Direction = direction;
        }

        public void Tick()
        {
            IReadOnlyList<CollisionInfo> collisions = _collisionTrigger.GetCollisions();

            if (collisions.Count > 0)
                HasHit = true;

            _model.Force.Value = _model.Direction * _model.Speed * Time.deltaTime;

            UpdatePositionByForce(_model.Force.Value);
        }
    }
}
