using UnityEngine;

namespace Code.Physics.Force
{
    public class PhysicForce
    {
        public Vector2 Point { get; }

        public Vector2 Direction { get; }

        public PhysicForce(Vector2 point, Vector2 direction)
        {
            Point = point;
            Direction = direction;
        }
    }
}
