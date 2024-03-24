using UnityEngine;

namespace Code.Transport.Car
{
    [RequireComponent(typeof(PhysicObjectUpdater), typeof(ObjectActivator))]
    public class CarView : MonoBehaviour, ITransportView
    {
        [field: SerializeField]
        public PhysicObjectUpdater Body { get; private set; }

        [field: SerializeField]
        public ObjectActivator Activator { get; private set; }

        [field: SerializeField]
        public Collider2D Collider { get; private set; }
    }
}