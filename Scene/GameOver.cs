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
using Constants;

namespace Scenes
{
  public class GameOver: Scene
  {
    public GameObject[] ActorsPool;
    public Tile[] TilesPool;
    public GameOver(uint[,] BackgroundRep, uint[,] ActorsRep): base(BackgroundRep, ActorsRep)
    {
      this.BackgroundRep = BackgroundRep;
      this.ActorsRep = ActorsRep;
    }
    override public void Update(GameTime gameTime)
    {
      // this.OnSceneChangeRequest(Constants.Scenes.MAIN_MENU);
    }
    override public void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(
        GraphicManager.Font, "Game Over", 
        new Vector2(Constants.General.WINDOW_WIDTH / 2 - GraphicManager.Font.MeasureString("Press Start").Length()/2, Constants.General.WINDOW_HEIGHT / 2 + 12),
        Color.Green
      );  
    }
    override public void Load()
    {

    }
    override public void Unload()
    {
      this.Components.Clear();
    }
    protected override void OnSceneChangeRequest(string nextScene)
    {
      base.OnSceneChangeRequest(nextScene);
    }
  }
}