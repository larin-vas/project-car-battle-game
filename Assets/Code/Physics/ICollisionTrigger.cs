using System.Collections.Generic;

namespace Code.Physics
{
    public interface ICollisionTrigger
    {
        public IReadOnlyList<CollisionInfo> GetCollisions();
    }
}