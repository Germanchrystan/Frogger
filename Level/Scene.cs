using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Entities;
using Prototypes;
using Components;
using Components.State;
using Managers;
using Components.Collisions;
using Microsoft.Xna.Framework.Graphics;
using System.Linq.Expressions;

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
    public Scene(uint[,] BackgroundRep, uint[,] ActorsRep, List<ColliderRelation> colliderRelations)
    {
      this.BackgroundRep = BackgroundRep;
      this.ActorsRep = ActorsRep;
      int TileLength = BackgroundRep.Length * BackgroundRep.GetLength(0);
      TilesPool = new Tile[TileLength];
      collisionManager = new CollisionManager(colliderRelations);
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

    public void Update(GameTime gameTime)
    {
      updaters.Update(gameTime);
      collisionManager.Update();
      timers.Update(gameTime);
      stateManagers.Update(null);
    }

    public void Render(SpriteBatch spriteBatch)
    {
      renderer.Render(spriteBatch);
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