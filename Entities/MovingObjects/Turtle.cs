using Microsoft.Xna.Framework;
using System.Collections.Generic;

using Prototypes;
using Components;
using Components.Collisions;
using Components.GraphicComponents;
using Components.State;
using Constants;
using Managers;

namespace Entities
{

  public class Turtle : GameObject, CollisionHandler
  {
    const int speed = 50;
    const int offset = 5;

    const string ABOVE_WATER = "ABOVE_WATER";
    const string SINKING = "SINKING";
    const string BELOW_WATER = "BELOW_WATER";
    const string RISING = "RISING";

    private State aboveWater = new State(ABOVE_WATER, new List<string>{SINKING});
    private State sinking = new State(SINKING, new List<string>{BELOW_WATER});
    private State belowWater = new State(BELOW_WATER, new List<string>{RISING});
    private State rising = new State(RISING,new List<string>{ABOVE_WATER});
    public void OnEndAboveWaterAnimation()
    {
      GetComponent<StateManager>(Constants.Components.STATE_MANAGER).SetState(SINKING);
    }
    public void OnEndSinkingAnimation()
    {
      GetComponent<StateManager>(Constants.Components.STATE_MANAGER).SetState(BELOW_WATER);
    }
    public void OnStartBelowWaterAnimation()
    {
      this.GetComponent<CollisionBox>(Constants.Components.COLLISION_BOX).Active = false;
    }
    public void OnEndBelowWaterAnimation()
    {
      GetComponent<StateManager>(Constants.Components.STATE_MANAGER).SetState(RISING);
      this.GetComponent<CollisionBox>(Constants.Components.COLLISION_BOX).Active = true;
    }
    public void OnEndRisingAnimation()
    {
      GetComponent<StateManager>(Constants.Components.STATE_MANAGER).SetState(ABOVE_WATER);
    }
    public Turtle(Vector2 position, int xDirection)
    {
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(General.SIZE, General.SIZE)));
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(Tags.TURTLE, this, this, new Vector2(offset, 0), new Vector2(General.SIZE - (offset * 2), General.SIZE)));
      Components.Add(Constants.Components.MOVEMENT, new Movement(speed, xDirection, 0));
      Components.Add(Constants.Components.STATE_MANAGER, new StateManager(this, aboveWater).AddState(sinking).AddState(belowWater).AddState(rising));
      Components.Add(Constants.Components.UPDATER, new MovingObjectUpdater(this));

      Animation AboveWaterAnimation = new Animation
      (
        ABOVE_WATER,
        false,
        new Frame[] 
        {
          new Frame(.2f, GraphicManager.GetFrameRectangle(0, 5)),
          new Frame(.2f, GraphicManager.GetFrameRectangle(1, 5)),
          new Frame(.2f, GraphicManager.GetFrameRectangle(2, 5))
        },
        null,
        OnEndAboveWaterAnimation
      );
      Animation SinkingAnimation = new Animation
      (
        SINKING,
        false,
        new Frame[]
        {
          new Frame(.2f, GraphicManager.GetFrameRectangle(3,5)),
          new Frame(.2f, GraphicManager.GetFrameRectangle(4,5)),
          new Frame(.2f, GraphicManager.GetFrameRectangle(5,5)),
          new Frame(.2f, GraphicManager.GetFrameRectangle(6,5)),
        },
        null,
        OnEndSinkingAnimation
      );
      Animation BelowWaterAnimation = new Animation
      (
        BELOW_WATER,
        false,
        new Frame[]
        {
          new Frame(.5f, GraphicManager.GetFrameRectangle(7,5)),
        },
        OnStartBelowWaterAnimation,
        OnEndBelowWaterAnimation
      );
      Animation RisingAnimation = new Animation
      (
        RISING,
        false,
        new Frame[]
        {
          new Frame(.2f, GraphicManager.GetFrameRectangle(6,5)),
          new Frame(.2f, GraphicManager.GetFrameRectangle(5,5)),
          new Frame(.2f, GraphicManager.GetFrameRectangle(4,5)),
          new Frame(.2f, GraphicManager.GetFrameRectangle(3,5)),
        },
        null,
        OnEndRisingAnimation
      );
      Components.Add(Constants.Components.ANIMATOR,
        new Animator(this, AboveWaterAnimation)
        .AddAnimation(SinkingAnimation)
        .AddAnimation(BelowWaterAnimation)
        .AddAnimation(RisingAnimation)
      );
      Components.Add(Constants.Components.RENDERER, new AnimatorRenderer(7, this));
    }
    public void OnCollision(object source, CollisionBox colliding){}
  }
}