using System.Collections.Generic;
using Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Prototypes;

namespace Components.GraphicComponents
{
  public struct FrameData
  {
    public Texture2D texture;
    public Rectangle rect;
  }
  public class Animator: TimeOutHandler, Component
  {
    private Dictionary<string, Animation> Animations = new Dictionary<string, Animation>(); 
    private string defaultAnimation;
    private string currentAnimation;
    private int currentFrameIndex = 0;
    private GameObject parent;
    private Timer timer;
    private Texture2D atlas;
    
    public Animator(GameObject parent)
    {
      this.timer = new Timer(1, "animation over", false, this);
      this.parent = parent;
      this.atlas = GraphicManager.Atlas; // TODO: Change for generic engine
    }

    public Animator(GameObject parent, Animation defaultAnimation)
    {
      this.defaultAnimation = defaultAnimation.Name;
      this.currentAnimation = defaultAnimation.Name;
      this.AddAnimation(defaultAnimation);
      this.timer = new Timer(defaultAnimation.GetCurrentFrameDuration(0), "animation over", true, this);
      this.parent = parent;
      this.atlas = GraphicManager.Atlas; // TODO: Change for generic engine
    }

    public Animator AddAnimation(Animation animation)
    {
      Animations.Add(animation.Name, animation);
      return this;
    }

    public void OnTimeOut(object source, string message)
    {
      float newTimerMax = this.NextFrame();
      
      if(newTimerMax > 0)
      {
        timer.SetMax(newTimerMax);
      }
      else
      {
        currentAnimation = defaultAnimation;
      }
    }
    public void ResetAnimation()
    {
      currentFrameIndex = 0;
    }
    private float NextFrame()
    {
      int framesLength = Animations[currentAnimation].Frames.Length;
      if ( framesLength > currentFrameIndex + 1)
      {
        currentFrameIndex++;
        return Animations[currentAnimation].Frames[currentFrameIndex].FrameDuration;
      }

      return -1;
    }
    public FrameData GetCurrentFrameData()
    {
      return new FrameData
      {
        rect = Animations[currentAnimation].Frames[currentFrameIndex].SpriteRect,
        texture = atlas,
      };
    }
  }
}