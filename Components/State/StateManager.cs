using System;
using System.Collections.Generic;
using System.Linq;
using Prototypes;

namespace Components.State
{
  public class StateManager : UpdateableIterableComponent<object>
  {
    public GameObject parent;
    private Dictionary<string, State> states= new Dictionary<string, State>();
    private List<string> stateRequestQueue = new List<string>();
    private State currentState;
    private State nextState;
    private State defaultState;
    public override string Type(){ return Constants.Components.STATE_MANAGER; }
    public event EventHandler<string> MessageSent;
    public StateManager(GameObject parent, State defaultState)
    {
      this.parent = parent;
      this.defaultState = defaultState;
      this.currentState = defaultState;
      this.nextState = defaultState;
      Active = true;
      this.AddState(defaultState);
    }
    public StateManager(GameObject parent)
    {
      this.parent = parent;
      this.currentState = null;
      this.nextState = null;
      this.defaultState = null;
      Active = true;
    }
    public string CurrentState { get { return currentState.Name; } }
    public void SetState(string state)
    {
      stateRequestQueue.Add(state);
    }

    private void proccessStateRequests()
    {
      foreach(string newState in stateRequestQueue)
      {
        if (nextState.IsNextStateAllowed(newState) && states.ContainsKey(newState))
        {
          State state = states[newState];
          nextState = state;
          defaultState = state;
        }
      }
      stateRequestQueue.Clear();
    }
    override public void Update(object nil)
    {
      proccessStateRequests();
      if (currentState != nextState && nextState.Message != null) OnSendMessage(nextState.Message);
      currentState = nextState;
      nextState = defaultState;
    }

    public StateManager AddState(State newState)
    {
      states.Add(newState.Name, newState);
      return this;
    }

    public void OnSendMessage(string message)
    {
      MessageSent?.Invoke(this, message);
    }
  }
}