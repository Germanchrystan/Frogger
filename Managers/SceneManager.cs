using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;
using Constants;

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
      Scene loadedScene;
      currentScene.Unload();
      // TODO: Load JSON
      switch (newScene)
      {
        case Constants.Scenes.LV1 :
          loadedScene = new Level(LevelTemplates.LVL1_B, LevelTemplates.LVL1_A, LevelTemplates.colliderRelations);
          SceneTransition(loadedScene);
          break;
        case Constants.Scenes.GAME_OVER:
          loadedScene = new GameOver(new uint[0,0]{}, new uint[0,0]{});
          SceneTransition(loadedScene);
          break;
        case Constants.Scenes.MAIN_MENU:
          loadedScene = new Menu(new uint[0,0]{}, new uint[0,0]{});
          SceneTransition(loadedScene);
          break;
        default:
          throw new ArgumentException("New level not found");
      }
    }
  }
}