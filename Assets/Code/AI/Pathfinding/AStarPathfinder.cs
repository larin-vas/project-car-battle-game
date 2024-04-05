using Code.Common;
using Code.Map;
using Code.Map.Tile;
using Code.Services;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Code.AI.Pathfinding
{
    public class AStarPathfinder : IPathfinder
    {
        private readonly Observable<TileModel>[,] _map;

        private readonly ConstantsService _constants;

        public AStarPathfinder(ConstantsService constants, MapController mapController)
        {
            _constants = constants;
            _map = mapController.GetMap();
        }

        public AStarPathfinder(ConstantsService constants, Observable<TileModel>[,] map)
        {
            _constants = constants;
            _map = map;
        }

        public IReadOnlyList<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
        {
            if (!IsCoordinatePassable(start) ||
                !IsCoordinatePassable(target))
                return null;

            HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
            SortedSet<PathNode> openSet = new SortedSet<PathNode>();

            PathNode startNode = new PathNode(start, 0, CalculateDistance(start, target), null);
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                PathNode currentNode = openSet.Min;
                openSet.Remove(currentNode);

                if (currentNode.Position == target)
                    return BuildPath(currentNode);

                closedSet.Add(currentNode.Position);

                foreach (Vector2Int neighbor in GetNeighbors(currentNode.Position))
                {
                    if (!IsCoordinatePassable(neighbor) || closedSet.Contains(neighbor))
                        continue;

                    float newCost = currentNode.GCost + CalculateDistance(currentNode.Position, neighbor);

                    PathNode neighborNode = new PathNode(neighbor, newCost, CalculateDistance(neighbor, target), currentNode);

                    if (!openSet.Contains(neighborNode) || newCost < neighborNode.GCost)
                        openSet.Add(neighborNode);
                }
            }

            return null;
        }

        private List<Vector2Int> GetNeighbors(Vector2Int position)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            foreach (Vector2Int direction in _constants.PathfinderAllowedDirections)
            {
                Vector2Int neighborPosition = position + direction;

                if (IsCoordinateCorrect(neighborPosition))
                    neighbors.Add(neighborPosition);
            }

            return neighbors;
        }

        private bool IsCoordinatePassable(Vector2Int position)
        {
            return
                IsCoordinateCorrect(position) &&
                _map[position.x, position.y].Value.Type == TileType.Ground;
        }

        private bool IsCoordinateCorrect(Vector2Int position)
        {
            return position.x >= 0 && position.x < _map.GetLength(0) &&
                   position.y >= 0 && position.y < _map.GetLength(1);
        }

        private float CalculateDistance(Vector2Int a, Vector2Int b)
        {
            return (a - b).magnitude;
        }

        private List<Vector2Int> BuildPath(PathNode currentNode)
        {
            List<Vector2Int> path = new List<Vector2Int>();

            while (currentNode != null)
            {
                path.Add(currentNode.Position);
                currentNode = currentNode.Parent;
            }

            path.Reverse();
            return path;
        }
    }
}