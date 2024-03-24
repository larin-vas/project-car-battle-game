using Code.Common;
using Code.Map;
using Code.Map.Tile;
using System.Collections.Generic;
using UnityEngine;

namespace Code.AI.Pathfinding
{
    public class AStarPathfinder : IPathfinder
    {
        private static readonly Vector2Int[] AllowedDirections =
        {
            new Vector2Int(-1, -1),
            new Vector2Int(-1,  0),
            new Vector2Int(-1,  1),

            new Vector2Int( 0, -1),
            new Vector2Int( 0,  1),

            new Vector2Int( 1, -1),
            new Vector2Int( 1,  0),
            new Vector2Int( 1,  1)
        };

        private readonly Observable<TileModel>[,] _map;

        public AStarPathfinder(MapController mapController)
        {
            _map = mapController.GetMap();
        }

        public AStarPathfinder(Observable<TileModel>[,] map)
        {
            _map = map;
        }

        public IReadOnlyList<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
        {
            List<Vector2Int> path = new List<Vector2Int>();

            HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
            SortedSet<PathNode> openSet = new SortedSet<PathNode>();

            openSet.Add(new PathNode(start, 0, Heuristic(start, target), null));

            while (openSet.Count > 0)
            {
                PathNode currentNode = openSet.Min;
                openSet.Remove(currentNode);

                if (currentNode.Position == target)
                {
                    // Построение пути
                    while (currentNode != null)
                    {
                        path.Add(currentNode.Position);
                        currentNode = currentNode.Parent;
                    }
                    path.Reverse();
                    return path;
                }

                closedSet.Add(currentNode.Position);

                foreach (Vector2Int neighbor in GetNeighbors(currentNode.Position))
                {
                    if (_map[neighbor.x, neighbor.y].Value.Type != TileType.Ground || closedSet.Contains(neighbor))
                        continue;

                    float newCost = currentNode.GCost + Heuristic(currentNode.Position, neighbor);

                    PathNode neighborNode = new PathNode(neighbor, newCost, Heuristic(neighbor, target), currentNode);

                    if (!openSet.Contains(neighborNode) || newCost < neighborNode.GCost)
                    {
                        openSet.Add(neighborNode);
                    }
                }
            }

            return null;
        }

        private List<Vector2Int> GetNeighbors(Vector2Int position)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            foreach (Vector2Int direction in AllowedDirections)
            {
                Vector2Int neighborPosition = position + direction;

                if (neighborPosition.x >= 0 && neighborPosition.x < _map.GetLength(0) &&
                    neighborPosition.y >= 0 && neighborPosition.y < _map.GetLength(1))
                {
                    neighbors.Add(neighborPosition);
                }
            }

            return neighbors;
        }

        private float Heuristic(Vector2Int a, Vector2Int b)
        {
            return (a - b).magnitude;
            //return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }
}