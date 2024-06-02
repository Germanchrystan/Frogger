using System;
using System.Linq;

using Prototypes;
using Constants;

namespace Components
{
  public class Counter: Component
  {
    public event EventHandler<string> LimitReached;
    private int currentValue;
    private int min;
    private int max;
    private string message;
    public int Value { get { return currentValue; } }
    public string Type() { return Constants.Components.COUNTER; } 

    public Counter(int currentValue, int min, int max, string message)
    {
      this.currentValue = currentValue;
      this.min = min;
      this.max = max;
      this.message = message;
    }
    public void DecreaseCounter()
    {
      currentValue--;
      if (currentValue <= min) OnLimitReached();
    }
    public void DecreaseCounter(int amount)
    {
      currentValue -= amount;
      if (currentValue <= min) OnLimitReached();
    }

    public void IncreaseCounter()
    {
      currentValue++;
      if (currentValue >= max) OnLimitReached();
    }
    public void IncreaseCounter(int amount)
    {
      currentValue += amount;
      if (currentValue >= max) OnLimitReached();
    }
    public void OnLimitReached()
    {
      LimitReached?.Invoke(this, message);
    }
  }
}