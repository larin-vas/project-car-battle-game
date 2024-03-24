using UnityEngine;

namespace Code.Map
{
    [RequireComponent(typeof(ObjectActivator))]
    public class MapView : MonoBehaviour
    {
        [field: SerializeField]
        public MapUpdater GroundMap{ get; private set; }

        [field: SerializeField]
        public MapUpdater HillMap { get; private set; }

        [field: SerializeField]
        public ObjectActivator Activator { get; private set; }

        [field: SerializeField]
        public GridLayout GridLayout { get; private set; }

        [field: SerializeField]
        public Collider2D Collider { get; private set; }
    }
}
