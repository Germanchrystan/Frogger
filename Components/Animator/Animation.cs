using System;
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
    public Action onStartAnimation;
    public Action onEndAnimation;

    public Animation(string Name, bool isLoop, Frame[] frames)
    {
      this.Name = Name;
      this.isLoop = isLoop;
      this.frames = frames;
      this.onStartAnimation = null;
      this.onEndAnimation = null;
    }
    public Animation(string Name, bool isLoop, Frame[] frames, Action onStartAnimation, Action onEndAnimation)
    {
      this.Name = Name;
      this.isLoop = isLoop;
      this.frames = frames;
      this.onStartAnimation = onStartAnimation;
      this.onEndAnimation = onEndAnimation;
    }
    public float GetCurrentFrameDuration(int frameIndex)
    {
      return frames[frameIndex].FrameDuration;
    }
  }
}