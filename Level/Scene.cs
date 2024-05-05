using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Entities;
using Prototypes;
using Components;
using Components.State;
using Managers;
using Components.Collisions;
using Microsoft.Xna.Framework.Graphics;

namespace Scenes
{
  public class Scene: GameObject
  {
    public uint[,] BackgroundRep;
    public uint[,] ActorsRep;
    public GameObject[] ActorsPool;
    public Tile[] TilesPool;

    private UpdateableIterableComponentList<Updater, GameTime> updaters = new UpdateableIterableComponentList<Updater, GameTime>();
    private CollisionManager collisionManager = new CollisionManager();
    private UpdateableIterableComponentList<StateManager, object> stateManagers = new UpdateableIterableComponentList<StateManager, object>();
    private UpdateableIterableComponentList<Timer, GameTime> timers = new UpdateableIterableComponentList<Timer, GameTime>();
    private Renderer renderer = new Renderer();

    private const string PLAYING_STATE_NAME = "PLAYING";
    private const string PAUSED_STATE_NAME = "PAUSED";
    private State playingState = new State(PLAYING_STATE_NAME, new List<string>{PAUSED_STATE_NAME});
    private State pausedState = new State(PAUSED_STATE_NAME, new List<string>{PLAYING_STATE_NAME});
    private StateManager stateManager;
    private Input input;

    private Dictionary<Keys, Command> levelBindings = new Dictionary<Keys, Command>()
    {
      {Keys.Enter, new Command(Constants.Commands.COMMAND_PAUSE)},
    };
    public Scene(uint[,] BackgroundRep, uint[,] ActorsRep, List<ColliderRelation> colliderRelations)
    {
      this.BackgroundRep = BackgroundRep;
      this.ActorsRep = ActorsRep;
      int TileLength = BackgroundRep.Length * BackgroundRep.GetLength(0);
      TilesPool = new Tile[TileLength];
      collisionManager = new CollisionManager(colliderRelations);
      stateManager = new StateManager(this, playingState).AddState(pausedState);
      input = new Input(levelBindings);
      // this.AddComponent(Constants.Components.STATE_MANAGER, );
    }
    public void Load()
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
      TilesPool = tmpTileList.ToArray();
      ActorsPool = tmpActorList.ToArray();
      foreach (GameObject go in TilesPool)
      {
        foreach (Component c in go.Components.Values)
        {
          HandleChildComponent(c);
        }
      }
      foreach (GameObject go in ActorsPool)
      {
        foreach (Component c in go.Components.Values)
        {
          HandleChildComponent(c);
        }
      }
    }

    public void LoadColliderRelations()
    {

    }

    private void gameCycle(GameTime gameTime)
    {
      updaters.Update(gameTime);
      collisionManager.Update();
      timers.Update(gameTime);
      stateManagers.Update(null);
    }

    public void Update(GameTime gameTime)
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

    public void Render(SpriteBatch spriteBatch)
    {
      renderer.Render(spriteBatch);
      if(stateManager.CurrentState == PAUSED_STATE_NAME) spriteBatch.Draw(GraphicManager.BlackTexture, new Rectangle(0, 0, 1000, 1000), new Color(0, 0, 0, 0.5f) ); 
    }

    public void Unload()
    {
      Array.Clear(TilesPool);
      Array.Clear(ActorsPool);

      updaters.Clear();
      //collisionManager.Clear();
      timers.Clear();
      stateManagers.Clear();
      renderer.Clear();
    }

    public void HandleChildComponent(Component component)
    {
      switch(component.Type())
      {
        case Constants.Components.UPDATER:
          updaters.AddComponent((Updater)component);
          break;
        case Constants.Components.COLLISION_BOX:
          collisionManager.Add((CollisionBox)component);
          break;
        case Constants.Components.TIMER:
          timers.AddComponent((Timer)component);
          break;
        case Constants.Components.STATE_MANAGER:
          stateManagers.AddComponent((StateManager)component);
          break;
        case Constants.Components.RENDERER:
          renderer.AddToList((Renderable)component);
          break;
        default:
          return;
      }
    }
  }
}