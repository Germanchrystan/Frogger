using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Components;
using Prototypes;

namespace Entities
{
  public class Tile: GameObject
  {
    public Tile(Texture2D texture, Vector2 position, Color color)
    {
      Components.Add(Constants.Components.TRANSFORM, new Transform(position, new Vector2(Constants.General.SIZE,Constants.General.SIZE)));
      Components.Add(Constants.Components.RENDERER, new Renderer(texture, color, 0, this));
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