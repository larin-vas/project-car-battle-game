using Code.Common.Interfaces;
using Code.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Code.Transport.Car.CarHealth
{
    public class CarHealthController : IActivatable, IHealthProvider, ITickable
    {
        private readonly CarHealthModel _model;

        private readonly ICollisionTrigger _collisionTrigger;

        public CarHealthController(
            CarHealthModel model,
            ICollisionTrigger collisionTrigger)
        {
            _model = model;
            _collisionTrigger = collisionTrigger;
        }

        public void Enable()
        {
            throw new System.NotImplementedException();
        }

        public void Disable()
        {
            throw new System.NotImplementedException();
        }

        public float GetCurrentHealth()
        {
            return _model.CurrentHealthPoints;
        }

        public void RestoreHealth()
        {
            _model.CurrentHealthPoints = _model.MaxHealthPoints;
        }

        public void Tick()
        {
            foreach (CollisionInfo collision in _collisionTrigger.GetCollisions())
            {
                UpdateHealthByCollision(collision);
            }
        }

        private void UpdateHealthByCollision(CollisionInfo collision)
        {
            float newHealthPoints = GetCurrentHealth() - collision.CollisionDamage;

            _model.CurrentHealthPoints = Mathf.Clamp(newHealthPoints, 0f, _model.MaxHealthPoints);
        }
    }
}