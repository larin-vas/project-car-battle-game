using Code.GameCamera;
using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Game Camera", menuName = "Game Camera")]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField]
        public CameraView CameraView { get; private set; }

        [field: SerializeField]
        public int CameraHeight { get; private set; }
    }
}