using UnityEngine;

namespace Code.Transport.Car
{
    public interface ITransportView
    {
        public PhysicObjectUpdater Body { get; }

        public ObjectActivator Activator { get; }

        public Collider2D Collider { get; }
    }
}