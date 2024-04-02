using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components.GraphicComponents
{
  public class Frame
  {
    private float frameDuration;
    public float FrameDuration { get { return frameDuration; }}
    private Rectangle spriteRect;
    public Action frameAction;
    public Rectangle SpriteRect { get { return spriteRect; }}
    public Frame(float frameDuration, Rectangle spriteRect)
    {
      this.frameDuration = frameDuration;
      this.spriteRect = spriteRect;
      this.frameAction = null;
    }
    public Frame(float frameDuration, Rectangle spriteRect, Action frameAction)
    {
      this.frameDuration = frameDuration;
      this.spriteRect = spriteRect;
      this.frameAction = frameAction;
    }
  }
}
