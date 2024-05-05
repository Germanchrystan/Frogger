using System;
using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;

namespace Managers
{
  class SceneManager
  {
    private Scene currentScene;
    
    public SceneManager(Scene currentScene)
    {
      this.currentScene = currentScene;
    }
    public void Start()
    {
      currentScene.Load();
    }
    public void SceneTransition(Scene nextScene)
    {
      currentScene.Unload();
      currentScene = nextScene;
      currentScene.Load();
    }
    public void Update(GameTime gameTime)
    {
      currentScene.Update(gameTime);
    }
    public void Render(SpriteBatch spriteBatch)
    {
      currentScene.Render(spriteBatch);
    }

    public void OnSceneTransitionRequest(object source, string newScene)
    {
      // Load JSON
    }
  }
}