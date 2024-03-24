using UnityEngine;

namespace Code.Common
{
    public class Transformation
    {
        public Observable<Vector2> Position { get; }

        public Observable<Quaternion> Rotation { get; }

        public Transformation() : this(Vector2.zero, Quaternion.identity)
        { }

        public Transformation(Vector2 position, Quaternion rotation)
        {
            Position = new Observable<Vector2>(position);
            Rotation = new Observable<Quaternion>(rotation);
        }
    }
}