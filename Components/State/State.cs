using System;
using System.Collections.Generic;
using Prototypes;

namespace Components.State
{
  public class State
  {
    public string Name;
    private List<string> allowedStates;
    public State(string name, List<string> allowedStates)
    {
      this.Name = name;
      this.allowedStates = allowedStates;
    }
    public State(string name)
    {
      this.Name = name;
      this.allowedStates = new List<string>{};
    }

    public void setAllowedStates(List<string> allowedStates)
    {
      this.allowedStates = allowedStates;
    }
    public bool IsNextStateAllowed (string nextState)
    {
      return allowedStates.Contains(nextState);
    }
  }
}