using Microsoft.Xna.Framework;

using Constants;
using Components;
using Components.Collisions;
using Prototypes;
using Managers;
using Components.GraphicComponents;

namespace Entities
{
  public class MovingObject : GameObject, CollisionHandler
  {
    public MovingObject(Vector2 position, Rectangle frameRect, string tag, int xDirection, int layer)
    {
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(Constants.General.SIZE,Constants.General.SIZE)));
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(tag, this, this));
      Components.Add(Constants.Components.MOVEMENT, new Movement(50, xDirection, 0));

      Components.Add(Constants.Components.RENDERER, new SingleFrameRenderer(new FrameData{ texture = GraphicManager.Atlas, rect = frameRect }, Color.White, 5, this));
      Components.Add(Constants.Components.UPDATER, new MovingObjectUpdater(this));
    }

    public void OnCollision(object source, CollisionBox colliding)
    {
      
    }
  }

  public class MovingObjects
  {
    public static GameObject NormalLog(Vector2 position, int direction)
    {
      return new MovingObject(position, GraphicManager.GetFrameRectangle(2,2), Tags.LOG, direction, 5);
    }
    public static GameObject NormalCar(Vector2 position, int direction)
    {
      return new MovingObject(position, GraphicManager.GetFrameRectangle(1,1), Tags.CAR, direction, 9);
    }
  }
}
