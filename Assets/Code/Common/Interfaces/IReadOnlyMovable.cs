using UnityEngine;

namespace Code.Common.Interfaces
{
    public interface IReadOnlyMovable
    {
        public Vector2 GetPosition();

        public Quaternion GetRotation();
    }
}
