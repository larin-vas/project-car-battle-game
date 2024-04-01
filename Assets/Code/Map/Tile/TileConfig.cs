using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Code.Map.Tile
{
    [Serializable]
    public class TileConfig
    {
        public TileBase ObjectTile => _objectTile;

        public TileBase GroundTile => _groundTile;

        public TileType Type => _type;

        public float ElasticityRate => _elasticityRate;
        public float FrictionRate => _frictionRate;


        [SerializeField]
        private TileBase _objectTile;

        [SerializeField]
        private TileBase _groundTile;

        [SerializeField]
        private TileType _type;

        [SerializeField, Range(0f, 1f)]
        private float _elasticityRate;

        [SerializeField, Range(0f, 1f)]
        private float _frictionRate;
    }
}