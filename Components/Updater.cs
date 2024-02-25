using Microsoft.Xna.Framework;
using Entities;
using Prefabs;
using System.Collections.Generic;

namespace Components
{
  public class Updater
  {
    public static List<UpdateableComponent> UpdaterList = new List<UpdateableComponent>();
    
    public static void Update(GameTime gameTime)
    {
      for(int i = 0; i < UpdaterList.Count;i++)
      {
        UpdaterList[i].Update(gameTime);
      }
    }
  }

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