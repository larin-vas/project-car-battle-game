using Code.Map;
using Code.Map.Tile;
using Code.Physics;
using Code.Infrastructure.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class MapFactory : IFactory<MapController>
    {
        private readonly IFactory<TileConfig, TileModel> _tileFactory;

        private readonly MapConfig _config;

        public MapFactory(
            IFactory<TileConfig, TileModel> tileFactory, 
            MapConfig config)
        {
            _tileFactory = tileFactory;

            _config = config;
        }

        public MapController Create()
        {
            if (_config == null)
                throw new System.NullReferenceException();

            MapView view = Object.Instantiate(_config.MapView);

            MapUpdater groundMap = view.GroundMap;
            MapUpdater hillMap = view.HillMap;

            int seed = (_config.UseConfigSeed) ? _config.Seed : Random.Range(0, 100000);

            MapModel model = new MapModel(
                seed, _config.Width, _config.Height, _config.MapScale,
                _config.HillsRatio, _config.BorderWidth);

            MapGenerator generator = new MapGenerator(model, _tileFactory, _config.GroundTileConfig, _config.HillTileConfig);
            //generator.Generate();

            groundMap.Construct(model.Map);
            hillMap.Construct(model.Map);

            CollisionTrigger collisionTrigger = new CollisionTrigger(view.Collider);

            MapController map = new MapController(model, view, generator, collisionTrigger);

            return map;
        }
    }
}