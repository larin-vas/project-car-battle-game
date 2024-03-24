using Code.Transport.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Combat.Gun
{
    [RequireComponent(typeof(PhysicObjectUpdater), typeof(ObjectActivator))]
    public class GunView : MonoBehaviour
    {
        [field: SerializeField]
        public PhysicObjectUpdater Transformable { get; private set; }

        [field: SerializeField]
        public ObjectActivator Activator { get; private set; }
    }
}
