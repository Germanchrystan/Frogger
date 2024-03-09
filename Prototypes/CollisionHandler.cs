using Components.Collisions;

namespace Prototypes
{
    public interface CollisionHandler
    {
        public void OnCollision(object source, CollisionBox colliding);
    }
}
