using Microsoft.Xna.Framework;

using Prototypes;
using Components;
using Components.Collisions;
using Components.GraphicComponents;
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
    public void OnEndAboveWaterAnimation()
    {
      GetComponent<StateManager>(Constants.Components.STATE_MANAGER).SetState(SINKING, true);
    }
    public void OnEndSinkingAnimation()
    {
      GetComponent<StateManager>(Constants.Components.STATE_MANAGER).SetState(BELOW_WATER, true);
    }
    public void OnStartBelowWaterAnimation()
    {
      CollisionBox.DeactivateColliderBox(this.GetComponent<CollisionBox>(Constants.Components.COLLISION_BOX));
    }
    public void OnEndBelowWaterAnimation()
    {
      GetComponent<StateManager>(Constants.Components.STATE_MANAGER).SetState(RISING, true);
      CollisionBox.ActivateColliderBox(this.GetComponent<CollisionBox>(Constants.Components.COLLISION_BOX));
    }
    public void OnEndRisingAnimation()
    {
      GetComponent<StateManager>(Constants.Components.STATE_MANAGER).SetState(ABOVE_WATER, true);
    }
    public Turtle(Vector2 position, int xDirection)
    {
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(General.SIZE, General.SIZE)));
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(Tags.TURTLE, this, this, new Vector2(offset, 0), new Vector2(General.SIZE - (offset * 2), General.SIZE)));
      Components.Add(Constants.Components.MOVEMENT, new Movement(speed, xDirection, 0));
      Components.Add(Constants.Components.STATE_MANAGER, new StateManager(this, ABOVE_WATER));
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