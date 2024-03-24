using UnityEngine;

namespace Code.Physics.Force
{
    public class ForceModel
    {
        public Vector2 Point { get; }

        public Vector2 Direction { get; }

        public ForceModel(Vector2 point, Vector2 direction)
        {
            Point = point;
            Direction = direction;
        }
    }
}
