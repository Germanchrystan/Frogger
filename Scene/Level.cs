using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Entities;
using Prototypes;
using Components;
using Components.State;
using Components.Collisions;
using Constants;
using Managers;

namespace Scenes
{
  public class Level: Scene
  {
    private Stream stream = new Stream();
    private const string PLAYING_STATE_NAME = "PLAYING";
    private const string PAUSED_STATE_NAME = "PAUSED";
    private State playingState = new State(PLAYING_STATE_NAME, new List<string>{PAUSED_STATE_NAME});
    private State pausedState = new State(PAUSED_STATE_NAME, new List<string>{PLAYING_STATE_NAME});
    private StateManager stateManager;
    private Input input;
    private Counter goalCounter;
    private Counter livesCounter;
    private List<ColliderRelation> colliderRelations;
    private Dictionary<Keys, Command> levelBindings = new Dictionary<Keys, Command>()
    {
      {Keys.Enter, new Command(Constants.Commands.COMMAND_PAUSE)},
    };
    public Level(uint[,] BackgroundRep, uint[,] ActorsRep, List<ColliderRelation> colliderRelations): base(BackgroundRep, ActorsRep)
    {
      this.BackgroundRep = BackgroundRep;
      this.ActorsRep = ActorsRep;
      this.colliderRelations = colliderRelations;
      int TileLength = BackgroundRep.Length * BackgroundRep.GetLength(0);
      stateManager = new StateManager(this, playingState).AddState(pausedState);
      input = new Input(levelBindings);
      // Counters
      goalCounter = new Counter(0, -1, 5, Constants.Scenes.MAIN_MENU);
      livesCounter = new Counter(5, 0, 6, Constants.Scenes.GAME_OVER);
      goalCounter.LimitReached += HandleMessage;
      livesCounter.LimitReached += HandleMessage;
      
      // this.AddComponent(Constants.Components.STATE_MANAGER, );
    }
    override public void Load()
    {
      List<Tile> tmpTileList = new List<Tile>();
      List<GameObject> tmpActorList = new List<GameObject>();

      for(int i = 0; i < 11; i++)
      {
        for(int j = 0; j < 11; j++)
        {
          Vector2 position = new Vector2(Constants.General.SIZE * j, Constants.General.SIZE * i);
          Tile newTile = TileLoader.IntToTile(BackgroundRep[i, j], position);
          GameObject newActor = ActorLoader.IntToActor(ActorsRep[i, j], position);
          if (newTile != null) tmpTileList.Add(newTile);
          if (newActor != null) tmpActorList.Add(newActor);
        }
      }
      stream.LoadTiles(tmpTileList);
      stream.LoadActors(tmpActorList);
      stream.LoadColliderRelations(colliderRelations);
      stream.LoadComponents();
      stream.MessageSent += HandleMessage;
    }

    private void gameCycle(GameTime gameTime)
    {
      stream.Update(gameTime);
    }
    override public void Update(GameTime gameTime)
    {
      string incomingCommand = input.CheckAndGetCommand();

      if (incomingCommand == Constants.Commands.COMMAND_PAUSE)
      {
        if(stateManager.CurrentState == PLAYING_STATE_NAME)
        {
          stateManager.SetState(PAUSED_STATE_NAME);
        }
        else
        {
          stateManager.SetState(PLAYING_STATE_NAME);
        }
      }
      if(stateManager.CurrentState == PLAYING_STATE_NAME)
      {
        gameCycle(gameTime);
      }
      stateManager.Update(null);
    }

    override public void Render(SpriteBatch spriteBatch)
    {
      stream.Render(spriteBatch);
      if(stateManager.CurrentState == PAUSED_STATE_NAME) spriteBatch.Draw(GraphicManager.BlackTexture, new Rectangle(0, 0, 1000, 1000), new Color(0, 0, 0, 0.5f) );
      spriteBatch.DrawString(
        GraphicManager.Font, "Lives " + livesCounter.Value, 
        new Vector2(10, Constants.General.WINDOW_HEIGHT - 30),
        Color.Green
      );
    }

    override public void Unload()
    {
      stream.Unload();
      this.Components.Clear();
    }

    public void HandleMessage(object sender, string message)
    {
      switch(message)
      {
        case Constants.Components.DEAD_LEVEL_MSG:
          livesCounter.DecreaseCounter();
          break;
        case Constants.Components.GOAL_LEVEL_MSG:
          goalCounter.IncreaseCounter();
          break;
        case Constants.Scenes.GAME_OVER:
          OnSceneChangeRequest(message);
          break;
        case Constants.Scenes.MAIN_MENU:
          OnSceneChangeRequest(message);
          break;
        default:
          Console.WriteLine("Unknown Message" + message);
          break;
      }
    }

    protected override void OnSceneChangeRequest(string nextScene)
    {
      base.OnSceneChangeRequest(nextScene);
    }
  }
}