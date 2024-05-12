using System;

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
      this.currentScene.SceneChangeRequest += HandleSceneChangeRequest;
    }
    public void Start()
    {
      currentScene.Load();
    }
    public void SceneTransition(Scene nextScene)
    {
      currentScene.Unload();
      currentScene = nextScene;
      this.currentScene.SceneChangeRequest += HandleSceneChangeRequest;
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
    public void HandleSceneChangeRequest(object source, string newScene)
    {
      currentScene.Unload();
      // TODO: Load JSON
      Scene level = new Level(LevelTemplates.LVL1_B, LevelTemplates.LVL1_A, LevelTemplates.colliderRelations);
      SceneTransition(level);
    }
  }
}