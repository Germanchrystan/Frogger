using Components.Collisions;

namespace Prefabs
{
    public interface CollisionHandler
    {
        public void OnCollision(object source, CollisionData collisionData);
    }
}
