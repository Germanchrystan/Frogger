using System;
using System.Collections.Generic;
using Prototypes;

namespace Components
{
  public class StateManager : Component
  {
    public static List<StateManager> StateManagerList = new List<StateManager>();
    public static void UpdateStates()
    {
      for(int i = 0; i < StateManagerList.Count; i++)
      {
        StateManagerList[i].UpdateState();
      }
    }
    public GameObject parent;
    private string currentState;
    private string nextState;
    private string defaultState;

    public StateManager(GameObject parent, string defaultState)
    {
      this.parent = parent;
      this.defaultState = defaultState;
      this.currentState = defaultState;
      this.nextState = defaultState;
      StateManagerList.Add(this);
    }

    public StateManager(GameObject parent)
    {
      this.parent = parent;
      this.currentState = null;
      this.nextState = null;
      this.defaultState = null;
      StateManagerList.Add(this);
    }

    public string GetState { get { return currentState; } }
    public void SetState(string state)
    {
      nextState = state;
    }

    public void UpdateState()
    {
      currentState = nextState;
      nextState = defaultState;
    }
  }
}