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
using Managers;

namespace Entities
{
  public class Player:  GameObject, CollisionHandler
  {
    int speed = 100;
    const int layer = 7;
    private Dictionary<Keys, Command> playerBindings = new Dictionary<Keys, Command>()
    {
      {Keys.Up, new Command(Commands.COMMAND_UP)},
      {Keys.Down, new Command(Commands.COMMAND_DOWN)},
      {Keys.Left, new Command(Commands.COMMAND_LEFT)},
      {Keys.Right, new Command(Commands.COMMAND_RIGHT)},
    };
    

    public void onStartDeadAnimation()
    {
      CollisionBox.DeactivateColliderBox(this.GetComponent<CollisionBox>(Constants.Components.COLLISION_BOX));
      Transform spawnPoint = this.GetComponent<Transform>(Constants.Components.SPAWN_POINT);
      this.GetComponent<Transform>(Constants.Components.TRANSFORM).Position = new Vector2(spawnPoint.Position.X, spawnPoint.Position.Y);
    }

    public void onEndDeadAnimation()
    {
      this.GetComponent<StateManager>(Constants.Components.STATE_MANAGER).SetState(PlayerConstants.PLAYING, true);
      CollisionBox.ActivateColliderBox(this.GetComponent<CollisionBox>(Constants.Components.COLLISION_BOX));
    }

    public Player(Vector2 position)
    {
      Components.Add(Constants.Components.SPAWN_POINT, new Transform(position, new Vector2(General.SIZE, General.SIZE)));
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(General.SIZE, General.SIZE)));
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(Tags.PLAYER, this, this, new Vector2(2,2), new Vector2(28, 28)));
      Components.Add(Constants.Components.STATE_MANAGER, new StateManager(this, PlayerConstants.PLAYING));
      Components.Add(Constants.Components.INPUT, new Input(playerBindings));
      Components.Add(Constants.Components.MOVEMENT, new Movement(speed, 0, 0));
      Components.Add(Constants.Components.UPDATER, new PlayerUpdater(this));
      
      Animation IdleAnimation = new Animation(PlayerConstants.PLAYING, true, new Frame[]{ new Frame(1, GraphicManager.GetFrameRectangle(0,0))});
      Animation DeadAnimation = new Animation(PlayerConstants.DEAD, false, new Frame[]{ new Frame(1, GraphicManager.GetFrameRectangle(3,0))}, onStartDeadAnimation, onEndDeadAnimation);

      Components.Add(Constants.Components.ANIMATOR, new Animator(this, IdleAnimation).AddAnimation(DeadAnimation));
      Components.Add(Constants.Components.RENDERER, new AnimatorRenderer(layer, this));
    }
    public void OnCollision(object source, CollisionBox colliding)
    {
      Movement movement = GetComponent<Movement>(Constants.Components.MOVEMENT);
      StateManager stateManager = GetComponent<StateManager>(Constants.Components.STATE_MANAGER);

      colliding.Parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      if (colliding.GetTag() == Tags.WATER)
      {
        stateManager.SetState(PlayerConstants.DEAD, true);
      }
      if (colliding.GetTag() == Tags.LOG)
      {
        Movement collidingMovement = colliding.Parent.GetComponent<Movement>(Constants.Components.MOVEMENT);
        movement.xDirection = collidingMovement.xDirection;
        movement.speed = collidingMovement.speed;
        stateManager.SetState(PlayerConstants.PLAYING, true);
      }
      if (colliding.GetTag() == Tags.CAR)
      {
        stateManager.SetState(PlayerConstants.DEAD, true);
      }
      if (colliding.GetTag() == Tags.LEAF)
      {
        stateManager.SetState(PlayerConstants.PLAYING, true);
        Transform spawnPoint = this.GetComponent<Transform>(Constants.Components.SPAWN_POINT);
        this.GetComponent<Transform>(Constants.Components.TRANSFORM).Position = new Vector2(spawnPoint.Position.X, spawnPoint.Position.Y); 
        // TODO: ADD TO LEVEL STATE
      }
    }
  }
}