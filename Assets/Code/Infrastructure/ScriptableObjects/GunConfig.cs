using Code.Combat.Gun;
using Code.Combat.Projectile;
using Code.Wheel;
using System;
using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{
    [Serializable]
    public class GunConfig
    {
        public GunView GunView => _gunView;

        public Vector2 LocalPosition => _localPosition;

        public float RotationSpeed => _rotationSpeed;

        public float ReloadTime => _reloadTime;

        public ProjectileConfig ProjectileConfig => _projectileConfig;

        [SerializeField]
        private GunView _gunView;

        [SerializeField]
        private Vector2 _localPosition;

        [SerializeField, Min(0f)]
        private float _rotationSpeed;

        [SerializeField, Min(0f)]
        private float _reloadTime;

        [SerializeField]
        private ProjectileConfig _projectileConfig;
    }
}