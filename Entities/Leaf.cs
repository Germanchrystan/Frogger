using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Prototypes;
using Components;
using Components.Collisions;
using Components.GraphicComponents;
using Components.State;
using Managers;
using Constants;

namespace Entities
{
  public class Leaf: GameObject, CollisionHandler
  {
    private const string AVAILABLE_STATE = "AVAILABLE";
    private const string TAKEN_STATE = "TAKEN";
    private State available = new State(AVAILABLE_STATE, new List<string>{TAKEN_STATE});
    private State taken = new State(TAKEN_STATE, new List<string>{});
    FrameData availableFrameData = new FrameData{ texture = GraphicManager.Atlas, rect = GraphicManager.GetFrameRectangle(0,2)};
    FrameData takenFrameData = new FrameData{ texture = GraphicManager.Atlas, rect = GraphicManager.GetFrameRectangle(1,2)};

    public Leaf(Vector2 position)
    {
      Components.Add(Constants.Components.STATE_MANAGER, new StateManager(this, available).AddState(taken));
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(Constants.General.SIZE,Constants.General.SIZE)));
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(Tags.LEAF, this, this, new Vector2(2,2), new Vector2(28, 28)));
      
      Components.Add(Constants.Components.RENDERER, new SingleFrameRenderer(availableFrameData, Color.White, 0, this));
    }
    public void OnCollision(object source, CollisionBox colliding)
    {
      StateManager stateManager = GetComponent<StateManager>(Constants.Components.STATE_MANAGER);
      CollisionBox collisionBox = GetComponent<CollisionBox>(Constants.Components.COLLISION_BOX);
      SingleFrameRenderer renderer = GetComponent<SingleFrameRenderer>(Constants.Components.RENDERER);

      if (colliding.GetTag() == Tags.PLAYER)
      {
        stateManager.SetState(TAKEN_STATE);
        collisionBox.SetTag(Tags.TAKEN_LEAF);
        renderer.SetNewFrameData(takenFrameData);
      }
    }
  }
}