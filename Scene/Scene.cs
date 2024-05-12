using System;

using Prototypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; // TODO: Add ColliderRelation in Collisions namespace

namespace Scenes
{
  abstract public class Scene: GameObject
  {
    public uint[,] BackgroundRep;
    public uint[,] ActorsRep;
    public event EventHandler<string> SceneChangeRequest;
    public Scene(uint[,] BackgroundRep, uint[,] ActorsRep)
    {
      this.BackgroundRep = BackgroundRep;
      this.ActorsRep = ActorsRep;
    }
    abstract public void Load();
    abstract public void Update(GameTime gameTime);
    abstract public void Render(SpriteBatch spriteBatch);
    abstract public void Unload();
    protected virtual void OnSceneChangeRequest(string nextScene)
    {
      SceneChangeRequest?.Invoke(this, nextScene);
    }
  }
}