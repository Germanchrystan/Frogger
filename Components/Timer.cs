using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Prototypes;

namespace Components
{
  public class Timer: Component
  {
    // Static
    private static List<Timer> Timers = new List<Timer>();
    private static int activeNum = 0;
    private static void AddActiveTimer(Timer newTimer)
    {
      Timers.Add(newTimer);
      ActivateTimer(newTimer);
    }
    private static void AddInactiveTimer(Timer newTimer)
    {
      Timers.Add(newTimer);
    }

    public static void ActivateTimer(Timer timer)
    {
      Timer firstInac = Timers[activeNum];
      if (firstInac != timer)
      {
        int newPos = Timers.Count - 1;
        Timer temp = firstInac;
        Timers[activeNum] = timer;
        Timers[newPos] =  temp;
      }
      activeNum++;
    }

    public static void DeactivateTimer(Timer timer)
    {
      Timer lastAct = Timers[activeNum - 1];
      int timerPos = Timers.IndexOf(timer);
      if (lastAct != timer)
      {
        Timer temp = lastAct;
        Timers[activeNum - 1] = timer;
        Timers[timerPos] =  temp;
      }
      activeNum--;
    }

    public static void UpdateTimers(GameTime gameTime)
    {
      for(int i = 0; i < activeNum; i++)
      {
        Timers[i].Update(gameTime);
      }
    }
  
    // Instance
    private float max;
    private float runningTime;
    private string message;
    public event EventHandler<string> OnTimeOut;
    public Timer(float max, string message, bool startActive, TimeOutHandler timeOutHandler)
    {
      this.max = max;
      this.runningTime = max;
      this.message = message;
      OnTimeOut += timeOutHandler.OnTimeOut;
      if (startActive) AddActiveTimer(this);
      else AddInactiveTimer(this);
    }

    private void ResetTimer() { runningTime = max; }

    private void Update(GameTime gameTime)
    {
      float dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
      runningTime -= dt;
      if (runningTime <= 0)
      {
        Timeout();
      }
    }
    private void Timeout()
    {
      if(OnTimeOut != null) OnTimeOut(this, message);
      ResetTimer();
      // SideTimeOutAction();
    }
  }
}