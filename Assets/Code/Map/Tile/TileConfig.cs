using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Code.Map.Tile
{
    [Serializable]
    public class TileConfig
    {
        [field: SerializeField]
        public TileBase ObjectTile { get; private set; }

        [field: SerializeField]
        public TileBase GroundTile { get; private set; }

        [field: SerializeField]
        public TileType Type { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float ElasticityRate { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float FrictionRate { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float AirResistance { get; private set; }
    }
}