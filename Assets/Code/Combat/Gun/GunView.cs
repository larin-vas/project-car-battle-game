using UnityEngine;

namespace Code.Combat.Gun
{
    [RequireComponent(typeof(PhysicObjectUpdater), typeof(ObjectActivator))]
    public class GunView : MonoBehaviour
    {
        [field: SerializeField]
        public PhysicObjectUpdater PhysicObjectUpdater { get; private set; }

        [field: SerializeField]
        public ObjectActivator Activator { get; private set; }
    }
}
