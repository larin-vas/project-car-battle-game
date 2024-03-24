using Code.Car;
using Code.Common;
using UnityEngine;

namespace Code.Common.Interfaces
{
    public interface IMovable
    {
        public Vector2 GetPosition();

        public Quaternion GetRotation();

        public void SetPosition(Vector2 position);

        public void SetRotation(Quaternion rotation);
    }
}
