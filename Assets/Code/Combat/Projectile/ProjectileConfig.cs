using Code.Wheel;
using System;
using UnityEngine;

namespace Code.Combat.Projectile
{
    [Serializable]
    public class ProjectileConfig
    {
        public ProjectileView ProjectileView => _projectileView;

        public float Damage => _damage;

        public float Speed => _speed;

        [SerializeField]
        private ProjectileView _projectileView;

        [SerializeField, Min(0f)]
        private float _damage;

        [SerializeField, Min(0f)]
        private float _speed;
    }
}
