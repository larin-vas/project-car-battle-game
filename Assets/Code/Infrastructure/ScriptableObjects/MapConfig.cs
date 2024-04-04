using Code.Map;
using Code.Map.Tile;
using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Map", menuName = "Configs/Map")]
    public class MapConfig : ScriptableObject
    {
        [field: SerializeField]
        public MapView MapView { get; private set; }

        [field: SerializeField]
        public MapBootstrapType BootstrapType { get; private set; }

        [field: SerializeField]
        public bool UseConfigSeed { get; private set; }

        [field: SerializeField]
        public int Seed { get; private set; }

        [field: SerializeField, Min(1)]
        public int Width { get; private set; }

        [field: SerializeField, Min(1)]
        public int Height { get; private set; }

        [field: SerializeField, Min(1f)]
        public float MapScale { get; private set; }

        [field: SerializeField, Min(1)]
        public int BorderWidth { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float HillsRatio { get; private set; }

        [field: SerializeField]
        public TileConfig GroundTileConfig { get; private set; }

        [field: SerializeField]
        public TileConfig HillTileConfig { get; private set; }
    }
}