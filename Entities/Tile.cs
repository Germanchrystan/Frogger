using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Components;
using Prototypes;
using Components.GraphicComponents;
using Managers;
using Components.Collisions;
using Constants;

namespace Entities
{
  public class Tile: GameObject
  {
    public Tile(FrameData frameData, Vector2 position, Color color)
    {
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(Constants.General.SIZE,Constants.General.SIZE)));
      Components.Add(Constants.Components.RENDERER, new SingleFrameRenderer(frameData, color, 0, this));
    }
  }

  public class Water: Tile, CollisionHandler
  {
    public Water(FrameData frameData, Vector2 position, Color color) : base (frameData, position, color)
    {
      Components.Add(Constants.Components.COLLISION_BOX, new CollisionBox(Tags.WATER, this, this));
    }
    public void OnCollision(object source, CollisionBox colliding)
    {}
  }

  public class Tiles
  {
    public static Tile StreetTile(int variant, Vector2 position)
    {
      FrameData frameData = new FrameData{ texture = GraphicManager.Atlas, rect = GraphicManager.GetFrameRectangle(variant, 3)};
      return new Tile(frameData, position, Color.White);
    }
    public static Tile WalkwayTile(int variant, Vector2 position)
    {
      FrameData frameData = new FrameData { texture = GraphicManager.Atlas, rect = GraphicManager.GetFrameRectangle(variant, 4)};
      return new Tile(frameData, position, Color.White);
    }
    public static Tile GrassTile(Vector2 position)
    {
      FrameData frameData = new FrameData { texture = GraphicManager.Atlas, rect = GraphicManager.GetFrameRectangle(7, 2)};
      return new Tile(frameData, position, Color.White);
    }
    public static Tile Water(int variant, Vector2 position)
    {
      FrameData frameData = new FrameData { texture = GraphicManager.Atlas, rect = GraphicManager.GetFrameRectangle(variant, 2)};
      return new Water(frameData, position, Color.White);
    }
    public static Tile NullTile(Vector2 position)
    {
      FrameData frameData = new FrameData { texture = GraphicManager.Atlas, rect = GraphicManager.GetFrameRectangle(3, 0)};
      return new Tile(frameData, position, Color.Black);
    }
  }
}