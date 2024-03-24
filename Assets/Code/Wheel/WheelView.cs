using UnityEngine;

namespace Code.Wheel
{
    [RequireComponent(typeof(PhysicObjectUpdater), typeof(ObjectActivator))]
    public class WheelView : MonoBehaviour
    {
        [field: SerializeField]
        public PhysicObjectUpdater Transformable { get; private set; }

        [field: SerializeField]
        public ObjectActivator Activator { get; private set; }
    }
}