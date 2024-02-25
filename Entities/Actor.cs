using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Components;
using Components.Collisions;
using Prefabs;

namespace Entities
{
  public class MovingObject : GameObject, CollisionHandler
  {
    public MovingObject(Texture2D texture, Vector2 position, Color color, string tag, int xDirection, int layer)
    {
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(Constants.General.SIZE,Constants.General.SIZE)));
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(tag, this, this));
      Components.Add(Constants.Components.MOVEMENT, new Movement(50, xDirection));
      Components.Add(Constants.Components.RENDERER, new Renderer(texture, color, 5, this));
      Components.Add(Constants.Components.UPDATER, new MovingObjectUpdater(this));
    }

    public void OnCollision(object source, CollisionData collisionData)
    {
      
    }
  }

  public class MovingObjects
  {
    public static GameObject NormalLog(Texture2D texture, Vector2 position, int direction)
    {
      return new MovingObject(texture, position, Color.Brown, Constants.Tags.LOG, direction, 5);
    }
    public static GameObject NormalCar(Texture2D texture, Vector2 position, int direction)
    {
      return new MovingObject(texture, position, Color.Red, Constants.Tags.CAR, direction, 9);
    }
  }
}
