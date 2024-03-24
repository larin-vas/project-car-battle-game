using Code.Map;
using Code.Map.Tile;
using UnityEngine;

namespace Code.Infrastructure.ScriptableObjects
{

    [CreateAssetMenu(fileName = "New Map", menuName = "Map")]
    public class MapConfig : ScriptableObject
    {
        public MapView MapView => _mapView;

        public MapBootstrapType BootstrapType => _mapBootstrap;

        public bool UseConfigSeed => _useConfigSeed;
        public int Seed => _seed;

        public int Width => _width;
        public int Height => _height;

        public float MapScale => _mapScale;

        public int BorderWidth => _borderWidth;

        public float Drag => _drag;

        public float HillsRatio => _hillsRatio;

        public TileConfig GroundTileConfig => _groundTileConfig;
        public TileConfig HillTileConfig => _hillTileConfig;

        [SerializeField]
        private MapView _mapView;

        [SerializeField]
        private MapBootstrapType _mapBootstrap;

        [SerializeField]
        private bool _useConfigSeed;

        [SerializeField]
        private int _seed;

        [SerializeField, Min(1)]
        private int _width;

        [SerializeField, Min(1)]
        private int _height;

        [SerializeField, Min(1)]
        private float _mapScale;

        [SerializeField, Min(1)]
        private int _borderWidth;

        [SerializeField, Min(0)]
        private float _drag;

        [SerializeField, Range(0, 1)]
        private float _hillsRatio;

        [SerializeField]
        private TileConfig _groundTileConfig;

        [SerializeField]
        private TileConfig _hillTileConfig;
    }
}