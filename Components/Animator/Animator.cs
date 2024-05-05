using System;
using System.Collections.Generic;
using Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Prototypes;
using Components.State;

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
    private StateManager stateManager;
    public string Type() { return Constants.Components.ANIMATOR; }
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
      parent.AddComponent("ANIMATION_TIMER", this.timer);
      this.atlas = GraphicManager.Atlas; // TODO: Change for generic engine
      this.stateManager = parent.GetComponent<StateManager>(Constants.Components.STATE_MANAGER);
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
        timer.ResetTimer();
      }
      else
      {
        if (Animations[currentAnimation].onEndAnimation != null) Animations[currentAnimation].onEndAnimation();
        // currentAnimation = defaultAnimation;
      }
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
    public void ResetAnimation()
    {
      currentFrameIndex = 0;
    }
    public void ResetTimer()
    {
      float newTimerMax = Animations[currentAnimation].Frames[currentFrameIndex].FrameDuration;
      timer.SetMax(newTimerMax);
      timer.ResetTimer();
    }
    public void SetAnimation(string newState)
    {
      if (!Animations.ContainsKey(newState)) return;
      currentAnimation = newState;
      ResetAnimation();
      ResetTimer();
      if (Animations[currentAnimation].onStartAnimation != null)
      {
        Animations[currentAnimation].onStartAnimation();
      }
    }
    public void checkAnimatorStatus()
    {
      if (stateManager.CurrentState != currentAnimation)
      {
        SetAnimation(stateManager.CurrentState);
      }
    }
    public FrameData GetCurrentFrameData()
    {
      checkAnimatorStatus();
      return new FrameData
      {
        rect = Animations[currentAnimation].Frames[currentFrameIndex].SpriteRect,
        texture = atlas,
      };
    }
  }
}