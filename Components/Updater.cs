using System;

using Microsoft.Xna.Framework;
using Prototypes;
using Components.State;
using System.Collections.Generic;

namespace Components
{
  public class Updater: UpdateableIterableComponent<GameTime>, Component
  {
    private Dictionary<string, List<Action<GameTime>>> stateActions = new Dictionary<string, List<Action<GameTime>>>();
    override public string Type() { return Constants.Components.UPDATER; } 

    public Updater(bool startActive, GameObject parent)
    {
      Active = startActive;
    }

    public void AddState(string name, List<Action<GameTime>> actions)
    {
      stateActions.Add(name, actions);
    }

    override public void Update(GameTime gameTime){}
  }
}