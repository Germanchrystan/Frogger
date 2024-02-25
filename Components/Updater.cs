using Microsoft.Xna.Framework;
using Prototypes;
using System.Collections.Generic;

namespace Components
{
  public class Updater
  {
    public static List<UpdateableComponent> UpdaterList = new List<UpdateableComponent>();
    
    public static void Update(GameTime gameTime)
    {
      for(int i = 0; i < UpdaterList.Count;i++)
      {
        UpdaterList[i].Update(gameTime);
      }
    }
  }
}