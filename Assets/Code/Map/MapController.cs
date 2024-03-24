using Code.Common;
using Code.Map.Tile;
using Code.Physics;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Map
{
    public class MapController : ITickable
    {
        private readonly MapModel _model;
        private readonly MapView _view;

        private readonly IMapGenerator _generator;

        private readonly ICollisionTrigger _collisionTrigger;

        public MapController(
            MapModel mapModel, MapView mapView,
            IMapGenerator generator,
            ICollisionTrigger collisionTrigger)
        {
            _model = mapModel;
            _view = mapView;

            _generator = generator;

            _collisionTrigger = collisionTrigger;
        }

        public void Enable()
        {
            _view.Activator.Enable();
        }

        public void Disable()
        {
            _view.Activator.Disable();
        }

        public Observable<TileModel>[,] GetMap()
        {
            return _model.Map;
        }

        public void SetSeed(int seed)
        {
            _model.Seed = seed;
        }

        public void Generate()
        {
            _generator.Generate();
        }

        public float GetGenerationProgress()
        {
            return _generator.GenerationProgress;
        }

        public void Tick()
        {
            IReadOnlyList<CollisionInfo> collisions = _collisionTrigger.GetCollisions();

            foreach (CollisionInfo collision in collisions)
            {
                Vector2Int position = (Vector2Int)_view.GridLayout.WorldToCell(collision.Point);

                if (!IsBorder(position))
                    DestroyTile(position);
            }
        }

        private void DestroyTile(Vector2Int position)
        {
            TileModel tile = _model.Map[position.x, position.y].Value;
            tile.Type = TileType.Ground;
            tile.ObjectTile = tile.GroundTile;

            _model.Map[position.x, position.y].Value = tile;
        }

        private bool IsBorder(Vector2 position)
        {
            return (position.x <= _model.BorderWidth ||
                    position.x >= _model.Width - _model.BorderWidth ||
                    position.y <= _model.BorderWidth ||
                    position.y >= _model.Width - _model.BorderWidth);
        }
    }
}
