using System;
using UnityEngine;

namespace Code.Combat.Projectile
{
    [Serializable]
    public class ProjectileConfig
    {
        [field: SerializeField]
        public ProjectileView ProjectileView { get; private set; }

        [field: SerializeField, Min(0f)]
        public float Damage { get; private set; }

        [field: SerializeField, Min(0f)]
        public float Speed { get; private set; }
    }
}
