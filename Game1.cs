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
    private Scene level;

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

        Texture2D whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
        whiteTexture.SetData<Color>(new Color[] { Color.White });

        level = new Scene(LevelTemplates.LVL1_B, LevelTemplates.LVL1_A, LevelTemplates.colliderRelations);
        level.Load();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) // || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        level.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        level.Render(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
