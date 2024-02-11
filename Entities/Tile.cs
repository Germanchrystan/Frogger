using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Components;
using Components.Collisions;
using Prefabs;

namespace Entities
{
  public class Tile
  {
    Transform transform;
    Renderer renderer;
    public Tile(Texture2D texture, Vector2 position, Color color)
    {
      transform = new Transform(position, new Vector2(Constants.General.SIZE,Constants.General.SIZE));
      renderer = new Renderer(texture, transform, color, 0);
    }
  }

  public class Tiles
  {
    public static Tile StreetTile(Texture2D texture, Vector2 position)
    {
      return new Tile(texture, position, Color.DarkGray);
    }
    public static Tile WalkwayTile(Texture2D texture, Vector2 position)
    {
      return new Tile(texture, position, Color.Gray);
    }
    public static Tile GrassTile(Texture2D texture, Vector2 position)
    {
      return new Tile(texture, position, Color.LightGreen);
    }
    public static Tile NullTile(Texture2D texture, Vector2 position)
    {
      return new Tile(texture, position, Color.Black);
    }
  }
}