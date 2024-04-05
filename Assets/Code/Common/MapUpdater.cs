using Code.Common;
using Code.Map.Tile;
using Code.Physics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class MapUpdater : MonoBehaviour, IPhysicObject
{
    public Vector2 Force { get; private set; }

    public float CollisionDamage { get; private set; }

    public float ElasticityRate { get; private set; }

    [SerializeField]
    private Tilemap _tilemap;

    [SerializeField]
    private List<TileType> _visualizedTileTypes;

    private Observable<TileModel>[,] _map;

    public void Construct(Observable<TileModel>[,] map)
    {
        _map = map;

        SubscribeToMapChanges();

        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                UpdateMap(_map[i, j].Value);
            }
        }
    }

    private void OnEnable()
    {
        Force = Vector2.zero;
        CollisionDamage = 0f;
        ElasticityRate = 0f;

        if (_map == null)
            return;

        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                UpdateMap(_map[i, j].Value);
            }
        }
    }

    private void OnDestroy()
    {
        if (_map == null)
            return;

        UnsubscribeToMapChanges();
    }

    public void UpdateMap(TileModel newTile)
    {
        if (_tilemap == null || newTile == null)
            return;

        if (_visualizedTileTypes.Contains(newTile.Type))
            _tilemap.SetTile((Vector3Int)newTile.Position, newTile.ObjectTile);

        else if (_visualizedTileTypes.Contains(TileType.Ground))
            _tilemap.SetTile((Vector3Int)newTile.Position, newTile.GroundTile);

        else
            _tilemap.SetTile((Vector3Int)newTile.Position, null);

        ElasticityRate = newTile.ElasticityRate;
    }

    private void SubscribeToMapChanges()
    {
        if (_map == null)
            return;

        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                _map[i, j].OnChanged += UpdateMap;
            }
        }
    }

    private void UnsubscribeToMapChanges()
    {
        if (_map == null)
            return;

        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                _map[i, j].OnChanged -= UpdateMap;
            }
        }
    }
}
