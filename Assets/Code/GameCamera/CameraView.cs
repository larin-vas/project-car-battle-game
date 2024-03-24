using UnityEngine;

namespace Code.GameCamera
{
    [RequireComponent(typeof(CameraUpdater), typeof(ObjectActivator))]
    public class CameraView : MonoBehaviour
    {
        [field: SerializeField]
        public CameraUpdater CameraUpdater { get; private set; }

        [field: SerializeField]
        public ObjectActivator Activator { get; private set; }

        [field: SerializeField]
        public Camera Camera { get; private set; }
    }
}