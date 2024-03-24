using System.Collections.Generic;
using UnityEngine;

namespace Code.AI.Pathfinding
{
    public interface IPathfinder
    {
        public IReadOnlyList<Vector2Int> FindPath(Vector2Int start, Vector2Int target);
    }
}