using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scenes;
using Managers;

namespace Frogger;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SceneManager sceneManager;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = Constants.General.WINDOW_WIDTH;
        _graphics.PreferredBackBufferHeight = Constants.General.WINDOW_HEIGHT;
        _graphics.ApplyChanges();
        GraphicManager.LoadContent(Content);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Texture2D blackTexture = new Texture2D(GraphicsDevice, 1, 1);
        blackTexture.SetData(new Color[] { Color.Black });
        GraphicManager.BlackTexture = blackTexture;

        // Scene level = new Level(LevelTemplates.LVL1_B, LevelTemplates.LVL1_A, LevelTemplates.colliderRelations);
        Scene menu = new Menu(new uint[0,0]{}, new uint[0,0]{});
        sceneManager = new SceneManager(menu);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) // || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        sceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        sceneManager.Render(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
