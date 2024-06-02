using System;
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
    private RenderTarget2D renderTarget;
    private Rectangle renderDestination;
    private bool isResizing;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        
        _graphics.PreferredBackBufferWidth = Constants.General.WINDOW_WIDTH;
        _graphics.PreferredBackBufferHeight = Constants.General.WINDOW_HEIGHT;
        _graphics.ApplyChanges();
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += HandleClientSizeChanged;
        
        IsMouseVisible = true;
    }

    private void HandleClientSizeChanged(object sender, EventArgs e)
    {
        if(!isResizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
        {
            isResizing = true;
            calculateRenderDestionation();
            isResizing = false;
        }
    }

    protected override void Initialize()
    {
        GraphicManager.LoadContent(Content);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        renderTarget = new RenderTarget2D(GraphicsDevice, Constants.General.WINDOW_WIDTH, Constants.General.WINDOW_HEIGHT);
        calculateRenderDestionation();

        Texture2D blackTexture = new Texture2D(GraphicsDevice, 1, 1);
        blackTexture.SetData(new Color[] { Color.Black });
        GraphicManager.BlackTexture = blackTexture;

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

    private void calculateRenderDestionation()
    {
        Point size = GraphicsDevice.Viewport.Bounds.Size;
        
        float scaleX = (float)size.X / renderTarget.Width;
        float scaleY = (float)size.Y / renderTarget.Height;
        float scale = Math.Min(scaleX, scaleY);

        renderDestination.Width = (int)(renderTarget.Width * scale);
        renderDestination.Height = (int)(renderTarget.Height * scale);

        renderDestination.X = (size.X - renderDestination.Width) / 2;
        renderDestination.Y = (size.Y - renderDestination.Height) / 2;
    }
    

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        GraphicsDevice.SetRenderTarget(renderTarget);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        sceneManager.Render(_spriteBatch);
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(renderTarget, renderDestination, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
