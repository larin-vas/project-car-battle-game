using Code.Map.Tile;
using Code.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Code.Map
{
    public class MapGenerator
    {
        private readonly MapModel _model;

        private readonly IFactory<TileConfig, TileModel> _tileFactory;

        private readonly TileConfig _groundTileConfig;
        private readonly TileConfig _hillTileConfig;

        public MapGenerator(
            MapModel mapModel,
            IFactory<TileConfig, TileModel> tileFactory,
            TileConfig groundTileConfig, TileConfig hillTileConfig)
        {
            _model = mapModel;

            _tileFactory = tileFactory;

            _groundTileConfig = groundTileConfig;
            _hillTileConfig = hillTileConfig;
        }

        public void Generate()
        {
            for (int x = 0; x < _model.Width; x++)
            {
                for (int y = 0; y < _model.Height; y++)
                {
                    float xCoord = (float)x / _model.Width * _model.MapScale;
                    float yCoord = (float)y / _model.Height * _model.MapScale;

                    float noiseValue = Mathf.PerlinNoise(xCoord + _model.Seed, yCoord + _model.Seed);

                    if (IsBorder(x, y))
                        noiseValue = 1f;

                    TileModel tile = (1f - noiseValue <= _model.HillsRatio) ?
                        _tileFactory.Create(_hillTileConfig) :
                        _tileFactory.Create(_groundTileConfig);
                    tile.Position = new Vector2Int(x, y);

                    _model.Map[x, y].Value = tile;
                }
            }
        }

        private bool IsBorder(int x, int y)
        {
            return (x <= _model.BorderWidth || x >= _model.Width - _model.BorderWidth ||
                    y <= _model.BorderWidth || y >= _model.Width - _model.BorderWidth);
        }
    }
}
