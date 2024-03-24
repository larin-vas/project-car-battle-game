using Code.Map.Tile;
using UnityEngine;
using Zenject;

namespace Code.Map
{
    public class MapGenerator : IMapGenerator
    {
        public float GenerationProgress => _generationProgress;

        private readonly MapModel _model;

        private readonly IFactory<TileConfig, TileModel> _tileFactory;

        private readonly TileConfig _groundTileConfig;
        private readonly TileConfig _hillTileConfig;

        private float _generationProgress;

        public MapGenerator(
            MapModel mapModel,
            IFactory<TileConfig, TileModel> tileFactory,
            TileConfig groundTileConfig, TileConfig hillTileConfig)
        {
            _model = mapModel;

            _tileFactory = tileFactory;

            _groundTileConfig = groundTileConfig;
            _hillTileConfig = hillTileConfig;

            _generationProgress = 0f;
        }

        public void Generate()
        {
            for (int x = 0; x < _model.Width; x++)
            {
                for (int y = 0; y < _model.Height; y++)
                {
                    GenerateTile(x, y);

                    UpdateProgressValue(x, y);
                }
            }
        }

        private void GenerateTile(int x, int y)
        {
            float noiseValue = CalculateNoise(x, y);

            TileConfig tileConfig = IsHillByNoise(noiseValue) ?
                _hillTileConfig : _groundTileConfig;

            TileModel tile = _tileFactory.Create(tileConfig);

            tile.Position = new Vector2Int(x, y);

            _model.Map[x, y].Value = tile;
        }

        private float CalculateNoise(int x, int y)
        {
            if (IsBorder(x, y))
                return 1f;

            float xNoise = (float)x / _model.Width * _model.MapScale;
            float yNoise = (float)y / _model.Height * _model.MapScale;

            return Mathf.PerlinNoise(xNoise + _model.Seed, yNoise + _model.Seed);
        }

        private bool IsHillByNoise(float noiseValue)
        {
            return (noiseValue >= 1f - _model.HillsRatio);
        }

        private bool IsBorder(int x, int y)
        {
            return (x <= _model.BorderWidth || x >= _model.Width - _model.BorderWidth ||
                    y <= _model.BorderWidth || y >= _model.Width - _model.BorderWidth);
        }

        private void UpdateProgressValue(int x, int y)
        {
            float totalTiles = _model.Width + _model.Height;
            float generatedTiles = x * _model.Height + y;

            _generationProgress = generatedTiles / totalTiles;
        }
    }
}
