using Code.Combat.Gun;
using Code.Combat.Projectile;
using System;
using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{
    [Serializable]
    public class GunConfig
    {
        [field: SerializeField]
        public GunView GunView { get; private set; }

        [field: SerializeField]
        public Vector2 LocalPosition { get; private set; }

        [field: SerializeField, Min(0f)]
        public float RotationSpeed { get; private set; }

        [field: SerializeField, Min(0f)]
        public float ReloadTime { get; private set; }

        [field: SerializeField]
        public ProjectileConfig ProjectileConfig { get; private set; }
    }
}