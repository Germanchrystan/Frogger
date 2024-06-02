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
  public class Stream: GameObject
  {
    public GameObject[] ActorsPool;
    public Tile[] TilesPool;
    private UpdateableIterableComponentList<Updater, GameTime> updaters = new UpdateableIterableComponentList<Updater, GameTime>();
    private CollisionManager collisionManager = new CollisionManager();
    private UpdateableIterableComponentList<StateManager, object> stateManagers = new UpdateableIterableComponentList<StateManager, object>();
    private UpdateableIterableComponentList<Timer, GameTime> timers = new UpdateableIterableComponentList<Timer, GameTime>();
    private Renderer renderer = new Renderer();
    private List<string> messages = new List<string>();
    public event EventHandler<string> MessageSent;

    public void LoadTiles(List<Tile> tmpTileList)
    {
      TilesPool = tmpTileList.ToArray();
    }

    public void LoadActors(List<GameObject> tmpActorList)
    {
      ActorsPool = tmpActorList.ToArray();
    }

    public void LoadComponents()
    {
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
    public void LoadColliderRelations(List<ColliderRelation> colliderRelations)
    {
      collisionManager = new CollisionManager(colliderRelations);
    }
    public void Unload()
    {
      updaters.Clear();
      // collisionManager.Clear();
      timers.Clear();
      stateManagers.Clear();
      renderer.Clear();

      Array.Clear(TilesPool);
      Array.Clear(ActorsPool);
    }

    public void Render(SpriteBatch spriteBatch)
    {
      renderer.Render(spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
      try
      {
        updaters.Update(gameTime);
        collisionManager.Update();
        timers.Update(gameTime);
        stateManagers.Update(null);
        processMessages();
      }
      catch (Exception e)
      {
        Console.WriteLine($"Stream Update fail: {e.Message}");
      }
    }
    public void HandleChildComponent(Component component)
    {
      switch (component.Type())
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
          StateManager curr = (StateManager)component;
          curr.MessageSent += HandleMessage;

          stateManagers.AddComponent((StateManager)component);
          break;
        case Constants.Components.RENDERER:
          renderer.AddToList((Renderable)component);
          break;
        default:
          return;
      }
    }
    private void HandleMessage(object sender, string message)
    {
      messages.Add(message);
    }
    public void processMessages()
    {
      if(messages.Count > 0)
      {
        foreach (var message in messages)
        {
          OnSendMessage(message);
        }
        messages.Clear();
      }
    }
    private void OnSendMessage(string message)
    {
      MessageSent?.Invoke(this, message);
    }
  }
}