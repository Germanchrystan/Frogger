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
    const int speed = 50;
    public MovingObject(Vector2 position, Rectangle frameRect, string tag, int xDirection, int layer, int offset)
    {
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(General.SIZE, General.SIZE)));
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(tag, this, this, new Vector2(offset,0), new Vector2(General.SIZE - (offset * 2),General.SIZE)));
      Components.Add(Constants.Components.MOVEMENT, new Movement(speed, xDirection, 0));

      Components.Add(Constants.Components.RENDERER, new SingleFrameRenderer(new FrameData{ texture = GraphicManager.Atlas, rect = frameRect }, Color.White, layer, this));
      Components.Add(Constants.Components.UPDATER, new MovingObjectUpdater(this));
    }

    public void OnCollision(object source, CollisionBox colliding)
    {
      
    }
  }

  public class MovingObjects
  {
    private const int LOG_OFFSET = 5;
    private const int CAR_OFFSET = 2;
    private const int LOG_LAYER = 5;
    private const int CAR_LAYER = 9;
    public static GameObject NormalLog(Vector2 position, int direction)
    {
      MovingObject log = new MovingObject(position, GraphicManager.GetFrameRectangle(2,2), Tags.LOG, direction, LOG_LAYER, LOG_OFFSET);
      log.GetComponent<CollisionBox>(Constants.Components.COLLISION_BOX);
      return log;
    }
    public static GameObject NormalCar(Vector2 position, int direction)
    {
      return new MovingObject(position, GraphicManager.GetFrameRectangle(1,1, direction > 0), Tags.CAR, direction, CAR_LAYER, CAR_OFFSET);
    }
  }
}
