using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Components;
using Components.Collisions;
using Prefabs;

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
  public class Player:  GameObject, CollisionHandler
  {
    private Dictionary<Keys, Command> playerBindings = new Dictionary<Keys, Command>()
    {
      {Keys.Up, new Command(Constants.Commands.COMMAND_UP)},
      {Keys.Down, new Command(Constants.Commands.COMMAND_DOWN)},
      {Keys.Left, new Command(Constants.Commands.COMMAND_LEFT)},
      {Keys.Right, new Command(Constants.Commands.COMMAND_RIGHT)},
    };
    
    int speed = 100;
    public Player(Texture2D texture, Vector2 position)
    {
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(Constants.General.SIZE,Constants.General.SIZE)));
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(Constants.Tags.PLAYER, this, this));
      Components.Add(Constants.Components.INPUT, new Input(playerBindings));
      Components.Add(Constants.Components.MOVEMENT, new Movement(speed, 0));
      Components.Add(Constants.Components.UPDATER, new PlayerUpdater(this));
      Components.Add(Constants.Components.RENDERER, new Renderer(texture, Color.Green, 7, this));
    }

    public void OnCollision(object source, CollisionData collisionData)
    {

    }
  }
}