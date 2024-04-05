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
            return new TileModel(
                config.ObjectTile, config.GroundTile, position, config.Type,
                config.ElasticityRate, config.FrictionRate, config.AirResistance);
        }
    }
}