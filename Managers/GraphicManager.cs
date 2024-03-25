using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Constants;

namespace Managers
{
  class GraphicManager
  {
    public static Texture2D Atlas;
    public static void LoadContent(ContentManager content)
    {
      Atlas = content.Load<Texture2D>("Frogger");
    }

    public static Rectangle GetFrameRectangle(int x, int y)
    {
      return new Rectangle(General.SIZE * x, General.SIZE * y, General.SIZE, General.SIZE);
    }
  }
}