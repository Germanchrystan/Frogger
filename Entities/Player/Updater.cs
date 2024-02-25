using Microsoft.Xna.Framework;

using Components;
using Prototypes;

namespace Entities
{
  public class PlayerUpdater: Component, UpdateableComponent
  {
    Input input;
    Transform transform;
    int xDirection = 0;
    int yDirection = 0;
    public PlayerUpdater(GameObject parent)
    {
      input = parent.GetComponent<Input>(Constants.Components.INPUT);
      transform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      Updater.UpdaterList.Add(this);
    }
    public void Update(GameTime gameTime)
    {
      string incomingCommand = input.CheckAndGetCommand();
      if (incomingCommand == null) return;   

      if (incomingCommand == Constants.Commands.COMMAND_UP)
      {
        yDirection = -1;
        if (transform.Position.Y > 0)
        transform.Position = new Vector2
        (
          transform.Position.X, transform.Position.Y + yDirection * Constants.General.SIZE
        );
        yDirection = 0;
        return;
      }
      if (incomingCommand == Constants.Commands.COMMAND_DOWN)
      {
        yDirection = 1;
        if (transform.Position.Y + Constants.General.SIZE < Constants.General.WINDOW_HEIGHT) 
        transform.Position = new Vector2
        (
          transform.Position.X, transform.Position.Y + yDirection * Constants.General.SIZE
        );
        yDirection = 0;
        return;
      }
      if (incomingCommand == Constants.Commands.COMMAND_LEFT) 
      {
        xDirection = -1;
        if (transform.Position.X > 0) 
        transform.Position = new Vector2
        (
          transform.Position.X + xDirection * Constants.General.SIZE, transform.Position.Y
        );
        xDirection = 0;
        return;
      }
      if (incomingCommand == Constants.Commands.COMMAND_RIGHT)
      {
        xDirection = 1;
        if(transform.Position.X + Constants.General.SIZE < Constants.General.WINDOW_WIDTH) 
        transform.Position = new Vector2
        (
          transform.Position.X + xDirection * Constants.General.SIZE, transform.Position.Y
        );
        xDirection = 0;
        return;
      }
    }
  }
}