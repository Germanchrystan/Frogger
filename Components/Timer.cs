using System;
using Microsoft.Xna.Framework;
using Prototypes;

namespace Components
{
  public class Timer: UpdateableIterableComponent<GameTime>, Component
  {
    private float max;
    private float runningTime;
    private string message;
    public event EventHandler<string> OnTimeOut;
    override public string Type() { return Constants.Components.TIMER; }
    public Timer(float max, string message, bool startActive, TimeOutHandler timeOutHandler)
    {
      this.max = max;
      this.runningTime = max;
      this.message = message;
      Active = startActive;
      OnTimeOut += timeOutHandler.OnTimeOut;
    }

    public void ResetTimer() { runningTime = max; }
    public void SetMax(float newMax) { max = newMax; }
    override public void Update(GameTime gameTime)
    {
      float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
      runningTime -= dt;
      if (runningTime <= 0)
      {
        Timeout();
      }
    }
    private void Timeout()
    {
      if (OnTimeOut != null) OnTimeOut(this, message);
      ResetTimer();
    }
  }
}