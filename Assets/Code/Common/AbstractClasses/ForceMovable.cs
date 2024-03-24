using Code.Common.Interfaces;
using UnityEngine;

namespace Code.Common.AbstractClasses
{
    public abstract class ForceMovable : IMovable
    {
        public abstract Vector2 GetPosition();

        public abstract Quaternion GetRotation();

        public abstract void SetPosition(Vector2 position);

        public abstract void SetRotation(Quaternion rotation);

        public void UpdatePositionByForce(Vector2 force)
        {
            Vector2 newPosition = GetPosition() + force * Time.deltaTime;
            SetPosition(newPosition);
        }
    }
}
