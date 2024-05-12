using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Entities;
using Prototypes;
using Components;
using Managers;
using Components.State;

namespace Scenes
{
  public class Menu: Scene
  {
    public GameObject[] ActorsPool;
    public Tile[] TilesPool;
    // private StateManager stateManager;
    private Input input;
    private State playingState = new State("PLAYING_STATE_NAME", new List<string>{});
     private Dictionary<Keys, Command> menuBindings = new Dictionary<Keys, Command>()
    {
      {Keys.Enter, new Command(Constants.Commands.START_COMMAND)},
    };
    public Menu(uint[,] BackgroundRep, uint[,] ActorsRep): base(BackgroundRep, ActorsRep) 
    {
      this.BackgroundRep = BackgroundRep;
      this.ActorsRep = ActorsRep;

      // stateManager = new StateManager(this, playingState);
      input = new Input(menuBindings);
    }
    override public void Load()
    {

    }
    override public void Update(GameTime gameTime)
    {
      string incomingCommand = input.CheckAndGetCommand();

      if (incomingCommand == Constants.Commands.START_COMMAND)
      {
        this.OnSceneChangeRequest("LV1");
      }
    }
    override public void Render(SpriteBatch spriteBatch)
    {
      spriteBatch.DrawString(
        GraphicManager.Font, "Frogger", 
        new Vector2(Constants.General.WINDOW_WIDTH / 2 - GraphicManager.Font.MeasureString("Frogger").Length()/2, Constants.General.WINDOW_HEIGHT / 2 - 12),
        Color.Green
      );

      spriteBatch.DrawString(
        GraphicManager.Font, "Press Start", 
        new Vector2(Constants.General.WINDOW_WIDTH / 2 - GraphicManager.Font.MeasureString("Press Start").Length()/2, Constants.General.WINDOW_HEIGHT / 2 + 12),
        Color.Green
      );

      spriteBatch.DrawString(
        GraphicManager.Font, "A clon developed by German Chrystan", 
        new Vector2(Constants.General.WINDOW_WIDTH / 2 - GraphicManager.Font.MeasureString("A clon developed by German Chrystan").Length()/2, Constants.General.WINDOW_HEIGHT - 30),
        Color.Green
      );
    }
    public override void Unload()
    {
      this.Components.Clear();
    }

    protected override void OnSceneChangeRequest(string nextScene)
    {
      base.OnSceneChangeRequest(nextScene);
    }
  }
}