using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

using Prefabs;

namespace Components
{
  public class Command
  {
    private string commandName;
    private bool isActive;
    public Command(string commandName)
    {
      this.commandName = commandName;
      this.isActive = true;
    }
    public string Name { get { return commandName; }}
    public bool IsActive { get { return isActive; } set { isActive = value; } }
  }

  public class Input: Component
  {
    private Dictionary<Keys, Command> bindings = new Dictionary<Keys, Command>();
    private int activeNum;
    private string incomingCommand = null;
    public Input(Dictionary<Keys, Command> bindings)
    {
      this.bindings = bindings;
      this.activeNum = bindings.Count();
    }
    public void SetActive(Keys key)
    {

    }
    public void SetInactive(Keys key)
    {
      
    }
    public void checkInput()
    {
      incomingCommand = null;
      checkReleasedKeys();
      KeyboardState kState = Keyboard.GetState();
      Keys[] keys = kState.GetPressedKeys();

      if (keys.Length > 0 && bindings.ContainsKey(keys[0]) && bindings[keys[0]].IsActive)
      {
        incomingCommand = bindings[keys[0]].Name;
        bindings[keys[0]].IsActive = false;
        return;
      }
      incomingCommand = "null";
      return;
    }
    public string CheckAndGetCommand()
    {
      checkInput();
      return incomingCommand;
    }
    // TODO: Optimize this!!
    void checkReleasedKeys()
    {
      KeyboardState kState = Keyboard.GetState();
      
      for (int i = 0; i < bindings.Count; i++) 
      {
        var item = bindings.ElementAt(i);
        var key = item.Key;
        if(kState.IsKeyUp(key)) item.Value.IsActive = true;
      }
    }
  }
}