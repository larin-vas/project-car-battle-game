using Code.Map.Tile;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class TileModelFactory : IFactory<TileConfig, TileModel>, IFactory<Vector2Int, TileConfig, TileModel>
    {
        public TileModelFactory()
        { }

        public TileModel Create(TileConfig config)
        {
            return Create(Vector2Int.zero, config);
        }

        public TileModel Create(Vector2Int position, TileConfig config)
        {
            if (config.Type == TileType.Ground)
                return new TileModel(config.ObjectTile, config.GroundTile, position, config.Type, config.FrictionRate);
            else
                return new TileModel(config.ObjectTile, config.GroundTile, position, config.Type, config.ElasticityRate);
        }
    }
}