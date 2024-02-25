using Microsoft.Xna.Framework;
using Components;
using Prototypes;


namespace Entities
{
  public class MovingObjectUpdater: Component, UpdateableComponent
  {
    private GameObject parent;
    public MovingObjectUpdater(MovingObject parent)
    {
      this.parent = parent;
      Updater.UpdaterList.Add(this);
    }
    public void Update(GameTime gameTime)
    {
      float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
      Movement movement = parent.GetComponent<Movement>(Constants.Components.MOVEMENT);
      Transform transform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      float xVelocity =  movement.speed * movement.xDirection * dt;
      transform.Position = new Vector2(transform.Position.X + xVelocity, transform.Position.Y);
      if (movement.xDirection < 0 && transform.Rect.X < -Constants.General.SIZE)
      {
        transform.Position = new Vector2(Constants.General.WINDOW_WIDTH, transform.Position.Y);
      }

      if (movement.xDirection > 0 && transform.Rect.X > Constants.General.WINDOW_WIDTH)
      {
        transform.Position = new Vector2(0 - Constants.General.SIZE, transform.Position.Y);
      }
    }
  }
}