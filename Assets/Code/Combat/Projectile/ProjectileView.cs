using UnityEngine;

namespace Code.Combat.Projectile
{
    [RequireComponent(typeof(PhysicObjectUpdater), typeof(ObjectActivator))]
    public class ProjectileView : MonoBehaviour
    {
        [field: SerializeField]
        public PhysicObjectUpdater Body { get; private set; }

        [field: SerializeField]
        public ObjectActivator Activator { get; private set; }

        [field: SerializeField]
        public Collider2D Collider { get; private set; }
    }
}
