using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Constants;

namespace Managers
{
  class GraphicManager
  {
    public static Texture2D Atlas;
    public static SpriteFont Font;
    public static Texture2D BlackTexture;
    public static void LoadContent(ContentManager content)
    {
      Atlas = content.Load<Texture2D>("Frogger");
      Font = content.Load<SpriteFont>("Font");
    }

    public static Rectangle GetFrameRectangle(int x, int y)
    {
      return new Rectangle(General.SIZE * x, General.SIZE * y, General.SIZE, General.SIZE);
    }
    public static Rectangle GetFrameRectangle(int x, int y, bool flipX)
    {
      if (!flipX) return new Rectangle(General.SIZE * x, General.SIZE * y, General.SIZE, General.SIZE);
      return new Rectangle(General.SIZE * x + General.SIZE, General.SIZE * y, -General.SIZE, General.SIZE);
    }
  }
}