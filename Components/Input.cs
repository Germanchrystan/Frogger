using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

using Prototypes;

namespace Components
{
  public enum CommandType
  {
    TRIGGER,
    CONTINUOUS,
  }
  public class Command
  {
    private string commandName;
    private bool isActive;
    private CommandType commandType;
    public Command(string commandName)
    {
      this.commandName = commandName;
      this.isActive = true;
      this.commandType = CommandType.TRIGGER;
    }
    
    public Command(string commandName, CommandType commandType)
    {
      this.commandName = commandName;
      this.isActive = true;
      this.commandType = commandType;
    }
    public string Name { get { return commandName; }}
    public bool IsActive { get { return isActive; } set { isActive = value; } }
    public bool IsTrigger { get { return commandType == CommandType.TRIGGER; } }
  }
  // Input Component
  public class Input: Component
  {
    public static List<Keys> GlobalCommands = new List<Keys>(); // TODO: Continue this
    private Dictionary<Keys, Command> bindings = new Dictionary<Keys, Command>();
    private int activeNum;
    private string incomingCommand = null;
    public string Type() { return Constants.Components.INPUT; } 
    
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
        if (bindings[keys[0]].IsTrigger) bindings[keys[0]].IsActive = false;
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