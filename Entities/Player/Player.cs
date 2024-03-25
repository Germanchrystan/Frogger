using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Components;
using Components.Collisions;
using Prototypes;
using Constants;
using Components.GraphicComponents;

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

    public Player(Vector2 position)
    {
      Components.Add(Constants.Components.SPAWN_POINT, new Transform(position, new Vector2(General.SIZE, General.SIZE)));
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(General.SIZE, General.SIZE)));
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(Tags.PLAYER, this, this, new Vector2(2,2), new Vector2(28, 28))); //, texture));
      Components.Add(Constants.Components.STATE_MANAGER, new StateManager(this, PlayerConstants.PLAYING));
      Components.Add(Constants.Components.INPUT, new Input(playerBindings));
      Components.Add(Constants.Components.MOVEMENT, new Movement(speed, 0, 0));
      Components.Add(Constants.Components.UPDATER, new PlayerUpdater(this));
      
      Animation IdleAnimation = new Animation("Idle", true, new Frame[]{ new Frame(1, new Rectangle(0, 0, General.SIZE, General.SIZE))});
      Components.Add(Constants.Components.ANIMATOR, new Animator(this, IdleAnimation));
      Components.Add(Constants.Components.RENDERER, new AnimatorRenderer(7, this));
    }

    public void OnCollision(object source, CollisionBox colliding)
    {
      Movement movement = GetComponent<Movement>(Constants.Components.MOVEMENT);
      StateManager stateManager = GetComponent<StateManager>(Constants.Components.STATE_MANAGER);

      colliding.Parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      if (colliding.GetTag() == Tags.LOG)
      {
        Movement collidingMovement = colliding.Parent.GetComponent<Movement>(Constants.Components.MOVEMENT);
        movement.xDirection = collidingMovement.xDirection;
        movement.speed = collidingMovement.speed;
      }
      if (colliding.GetTag() == Tags.CAR)
      {
        stateManager.SetState(PlayerConstants.DEAD);
      }
    }
  }
}