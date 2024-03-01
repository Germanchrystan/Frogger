using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Components;
using Components.Collisions;
using Prototypes;
using Constants;
using System;

namespace Entities
{
  public class Player:  GameObject, CollisionHandler
  {
    private Dictionary<Keys, Command> playerBindings = new Dictionary<Keys, Command>()
    {
      {Keys.Up, new Command(Commands.COMMAND_UP)},
      {Keys.Down, new Command(Commands.COMMAND_DOWN)},
      {Keys.Left, new Command(Commands.COMMAND_LEFT)},
      {Keys.Right, new Command(Commands.COMMAND_RIGHT)},
    };
    
    int speed = 100;
    public Player(Texture2D texture, Vector2 position)
    {
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(General.SIZE, General.SIZE)));
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(Tags.PLAYER, this, this, new Vector2(2,2), new Vector2(28, 28))); //, texture));
      Components.Add(Constants.Components.INPUT, new Input(playerBindings));
      Components.Add(Constants.Components.MOVEMENT, new Movement(speed, 0));
      Components.Add(Constants.Components.UPDATER, new PlayerUpdater(this));
      Components.Add(Constants.Components.RENDERER, new Renderer(texture, Color.Green, 7, this));
    }

    public void OnCollision(object source, CollisionData collisionData)
    {
      Console.WriteLine(collisionData.ColliderTag);
      if (collisionData.ColliderTag == Tags.LOG)
      {
        // this.GetComponent<Movement>(Constants.Components.MOVEMENT).yDirection += 
      }
    }
  }
}