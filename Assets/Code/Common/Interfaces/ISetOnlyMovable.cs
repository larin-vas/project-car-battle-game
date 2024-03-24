using UnityEngine;

namespace Code.Common.Interfaces
{
    public interface ISetOnlyMovable
    {
        public void SetPosition(Vector2 position);

        public void SetRotation(Quaternion rotation);
    }
}
