using Microsoft.Xna.Framework;

using Components;
using Components.State;
using Prototypes;
using System;

namespace Entities
{
  public class PlayerUpdater: Updater
  {
    Input input;
    Transform transform;
    Movement movement;
    StateManager stateManager;
    int xDirection = 0;
    int yDirection = 0;
    
    public PlayerUpdater(bool startActive, GameObject parent): base(startActive, parent)
    {
      input = parent.GetComponent<Input>(Constants.Components.INPUT);
      transform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      movement = parent.GetComponent<Movement>(Constants.Components.MOVEMENT);
      stateManager = parent.GetComponent<StateManager>(Constants.Components.STATE_MANAGER);
    }
    override public void Update(GameTime gameTime)
    {
      if (stateManager.CurrentState == PlayerConstants.PLAYING)
      {
        PlayingStateUpdate(gameTime);
      }
      if (stateManager.CurrentState == PlayerConstants.DEAD)
      { 
        DeadStateUpdate(gameTime);
      }
    }
    public void PlayingStateUpdate(GameTime gameTime)
    {
      float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
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

      // Log movement
      if (movement.xDirection < 0)
      {
        float newXMovmenet = Math.Max(0, transform.Position.X + movement.xDirection * movement.speed * dt);
        transform.Position = new Vector2(newXMovmenet, transform.Position.Y);
      }
      if (movement.xDirection > 0)
      {
        float newXMovmenet = Math.Min(Constants.General.WINDOW_WIDTH - Constants.General.SIZE, transform.Position.X + movement.xDirection * movement.speed * dt);
        transform.Position = new Vector2(newXMovmenet, transform.Position.Y);
      }
      movement.resetMovement();
    }

    public void DeadStateUpdate(GameTime gameTime)
    {
    }
  }
}