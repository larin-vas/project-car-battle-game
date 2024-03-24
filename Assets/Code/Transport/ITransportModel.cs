using Code.Common;
using UnityEngine;

namespace Code.Transport
{
    public interface ITransportModel
    {
        public Transformation Transformation { get; }

        public Observable<Vector2> InertiaVector { get; }

        public Vector2 AccelerationVector { get; }

        public float CurrentAcceleration { get; }

        public float MaxAcceleration { get; }
        public float AccelerationRate { get; }

        public Vector2 CurrentMassCenter { get; }
        public Vector2 BaseMassCenter { get; }
        public float Mass { get; }
    }
}
