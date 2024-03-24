using System;
using UnityEngine;

namespace Code.AI.Pathfinding
{
    public class PathNode : IComparable<PathNode>
    {
        public Vector2Int Position { get; }

        public float GCost { get; }
        public float HCost { get; }

        public PathNode Parent { get; }

        public float FCost => GCost + HCost;

        public PathNode(Vector2Int position, float gCost, float hCost, PathNode parent)
        {
            Position = position;

            GCost = gCost;
            HCost = hCost;

            Parent = parent;
        }

        public int CompareTo(PathNode other)
        {
            int compare = FCost.CompareTo(other.FCost);
            if (compare == 0)
            {
                compare = HCost.CompareTo(other.HCost);
            }
            return compare;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            PathNode other = (PathNode)obj;
            return Position.Equals(other.Position);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}