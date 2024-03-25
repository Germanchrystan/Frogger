using Microsoft.Xna.Framework;

using Components;
using Prototypes;
using System;

namespace Entities
{
  public class PlayerUpdater: Component, UpdateableComponent, TimeOutHandler
  {
    Input input;
    Transform transform, spawnPoint;
    Movement movement;
    StateManager stateManager;
    Timer toRespawnTimer;
    int xDirection = 0;
    int yDirection = 0;
    public PlayerUpdater(GameObject parent)
    {
      input = parent.GetComponent<Input>(Constants.Components.INPUT);
      transform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      movement = parent.GetComponent<Movement>(Constants.Components.MOVEMENT);
      stateManager = parent.GetComponent<StateManager>(Constants.Components.STATE_MANAGER);
      spawnPoint = parent.GetComponent<Transform>(Constants.Components.SPAWN_POINT);
      Updater.UpdaterList.Add(this);
      toRespawnTimer = new Timer(1, "RESPAWN", false, this);
    }
    public void Update(GameTime gameTime)
    {
      if (stateManager.GetState == PlayerConstants.PLAYING)
      {
        PlayingStateUpdate(gameTime);
      }
      if (stateManager.GetState == PlayerConstants.DEAD)
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
      stateManager.SetState(PlayerConstants.PLAYING);
    }

    public void DeadStateUpdate(GameTime gameTime)
    {
      if (!toRespawnTimer.IsActive()) Timer.ActivateTimer(toRespawnTimer);
      stateManager.SetState(PlayerConstants.DEAD);
    }

    public void OnTimeOut(object source, string message)
    {
      if (message == "RESPAWN")
      {
        stateManager.SetState(PlayerConstants.PLAYING);
        transform.Position = new Vector2(spawnPoint.Position.X, spawnPoint.Position.Y);
        Timer.DeactivateTimer(toRespawnTimer);
      }
    }
  }
}