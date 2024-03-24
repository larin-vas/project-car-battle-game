using Code.Combat.Projectile;
using Code.Common.Interfaces;
using Code.Infrastructure.Pools;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Combat.Gun
{
    public class GunController : IMovable, ITickable
    {
        private IAimingInput _input;

        private readonly GunModel _model;
        private readonly GunView _view;

        private readonly IPool<ProjectileController> _projectilePool;

        public GunController(IAimingInput aimingInput,
            GunModel model, GunView view,
            IPool<ProjectileController> projectilePool)
        {
            _input = aimingInput;

            _model = model;
            _view = view;

            _projectilePool = projectilePool;
        }

        public void Enable()
        {
            _view.Activator.Enable();
        }

        public void Disable()
        {
            _view.Activator.Disable();
        }

        public Vector2 GetPosition()
        {
            return _model.Transformation.Position.Value;
        }

        public Quaternion GetRotation()
        {
            return _model.Transformation.Rotation.Value;
        }

        public void SetPosition(Vector2 position)
        {
            SetAbsolutePosition((Vector3)position + _model.Transformation.Rotation.Value * Quaternion.Inverse(_model.LocalRotation) * _model.LocalPosition);
        }

        public void SetRotation(Quaternion rotation)
        {
            SetAbsoluteRotation(rotation * _model.LocalRotation);
        }

        public void SetInput(IInput input)
        {
            _input = input;
        }


        private List<ProjectileController> _projectiles = new List<ProjectileController>();
        public void Tick()
        {
            if (_model.ReloadTimeRemaining > 0f)
            {
                float newReloadTimeRemaining = _model.ReloadTimeRemaining - Time.deltaTime;
                newReloadTimeRemaining = Mathf.Clamp(newReloadTimeRemaining, 0f, _model.ReloadTime);

                _model.ReloadTimeRemaining = newReloadTimeRemaining;
            }


            Vector2 direction = Vector2.zero;
            if (_input.Direction.magnitude > 2f)
                direction = ScreenCoordsToDirection(_input.Direction);
            else
                direction = _input.Direction;

            Quaternion newRotation = Quaternion.FromToRotation(_model.Transformation.Rotation.Value * Quaternion.Inverse(_model.LocalRotation) * Vector3.up, direction);

            SetLocalRotation(Quaternion.Lerp(_model.LocalRotation, newRotation, _model.RotationSpeed * Time.deltaTime));

            if (_input.Shoot && _model.ReloadTimeRemaining == 0f)
            {
                Debug.Log("POW!");
                ProjectileController projectile = _projectilePool.GetFromPool();
                projectile.SetPosition(GetPosition());
                projectile.SetRotation(GetRotation());
                projectile.SetDirection(GetRotation() * Vector2.up);

                _projectiles.Add(projectile);

                _model.ReloadTimeRemaining = _model.ReloadTime;
            }

            List<ProjectileController> projectiles = new List<ProjectileController>();

            foreach (var projectile in _projectiles)
            {
                projectile.Tick();

                if (projectile.HasHit)
                {
                    _projectilePool.ReturnToPool(projectile);
                    projectiles.Add(projectile);
                }
            }
            foreach (var projectile in projectiles)
            {
                _projectiles.Remove(projectile);
            }
        }

        private void SetAbsolutePosition(Vector2 position)
        {
            _model.Transformation.Position.Value = position;
        }

        private void SetAbsoluteRotation(Quaternion rotation)
        {
            _model.Transformation.Rotation.Value = rotation;
        }

        private void SetLocalRotation(Quaternion localRotation)
        {
            _model.LocalRotation = localRotation;
        }

        private Vector2 ScreenCoordsToDirection(Vector2 screenCoords)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(screenCoords);
            direction -= GetPosition();

            return direction;
        }
    }
}
