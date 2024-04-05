using UnityEngine;
using UnityEngine.Tilemaps;

namespace Code.Map.Tile
{
    public class TileModel
    {
        public TileBase ObjectTile { get; set; }

        public TileBase GroundTile { get; }

        public Vector2Int Position { get; set; }

        public TileType Type { get; set; }

        public float ElasticityRate { get; }

        public float FrictionRate { get; }

        public float AirResistance { get; }

        public TileModel(
            TileBase objectTile, TileBase groundTile, TileType type,
            float elasticityRate, float frictionRate, float airResistance) :
            this(objectTile, groundTile, Vector2Int.zero, type, elasticityRate, frictionRate, airResistance)
        { }

        public TileModel(
            TileBase objectTile, TileBase groundTile,
            Vector2Int position, TileType type,
            float elasticityRate, float frictionRate, float airResistance)
        {
            ObjectTile = objectTile;
            GroundTile = groundTile;

            Position = position;
            Type = type;

            AirResistance = airResistance;

            ElasticityRate = elasticityRate;

            FrictionRate = frictionRate;
        }
    }
}
