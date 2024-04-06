using Code.Combat.Projectile;
using Code.Infrastructure.Pools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Combat.Gun
{
    public class GunController : IWeapon
    {
        private IAimingInput _input;

        private readonly GunModel _model;
        private readonly GunView _view;

        private readonly IPool<ProjectileController> _projectilePool;

        public GunController(
            IAimingInput aimingInput,
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
            Vector2 absolutePosition =
                (Vector3)position + GetRotationWithoutLocal() * _model.LocalPosition;

            SetAbsolutePosition(absolutePosition);
        }

        public void SetRotation(Quaternion rotation)
        {
            SetAbsoluteRotation(rotation * _model.LocalRotation);
        }

        public void SetInput(IAimingInput input)
        {
            _input = input;
        }

        public void Tick()
        {
            if (_model.ReloadTimeRemaining > 0f)
                UpdateReloadTimeRemaining();

            UpdateRotation();

            if (_input.Shoot && CanShoot())
            {
                SpawnProjectile();

                _model.ReloadTimeRemaining = _model.ReloadTime;
            }

            UpdateActiveProjectiles();
        }

        private void UpdateRotation()
        {
            Vector2 aimDirection = _input.AimTargetPoint - GetPosition();

            Quaternion finalAimRotation =
                Quaternion.FromToRotation(GetRotationWithoutLocal() * Vector3.up, aimDirection);

            Quaternion newLocalRotation =
                Quaternion.Lerp(_model.LocalRotation, finalAimRotation, _model.RotationSpeed * Time.deltaTime);

            SetLocalRotation(newLocalRotation);
        }

        private void UpdateActiveProjectiles()
        {
            List<ProjectileController> activeProjectiles = _projectilePool.GetAllActiveObjects().ToList();

            foreach (ProjectileController projectile in activeProjectiles)
            {
                if (projectile.HasHit)
                    _projectilePool.ReturnToPool(projectile);
                else
                    projectile.Tick();
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

        private bool CanShoot()
        {
            return Mathf.Approximately(_model.ReloadTimeRemaining, 0f);
        }

        private void UpdateReloadTimeRemaining()
        {
            float newReloadTimeRemaining = _model.ReloadTimeRemaining - Time.deltaTime;
            newReloadTimeRemaining = Mathf.Clamp(newReloadTimeRemaining, 0f, _model.ReloadTime);

            _model.ReloadTimeRemaining = newReloadTimeRemaining;
        }

        private void SpawnProjectile()
        {
            ProjectileController projectile = _projectilePool.GetFromPool();
            projectile.SetPosition(GetPosition());
            projectile.SetRotation(GetRotation());
            projectile.SetDirection(GetRotation() * Vector2.up);
        }

        private Quaternion GetRotationWithoutLocal()
        {
            return _model.Transformation.Rotation.Value * Quaternion.Inverse(_model.LocalRotation);
        }
    }
}
