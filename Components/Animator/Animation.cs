using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Components.GraphicComponents
{
  public class Animation
  {
    public string Name;
    private bool isLoop;
    private Frame[] frames;
    public bool IsLoop { get { return isLoop; }}
    public Frame[] Frames { get { return frames; }}

    public Animation(string Name, bool isLoop, Frame[] frames)
    {
      this.Name = Name;
      this.isLoop = isLoop;
      this.frames = frames;
    }
    public float GetCurrentFrameDuration(int frameIndex)
    {
      return frames[frameIndex].FrameDuration;
    }
  }
}