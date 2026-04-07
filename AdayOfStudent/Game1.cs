using AdayOfStudent.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdayOfStudent;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SceneManager _sceneManager;
    
    private MenuScene _menuScene;
    private GameplayScene _gameplayScene;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.IsFullScreen = false; // Пока окно, можно потом сделать true
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _sceneManager = new SceneManager();
        
        _menuScene = new MenuScene(this);
        _gameplayScene = new GameplayScene(this);
            
        _sceneManager.AddScene("menu", _menuScene);
        _sceneManager.AddScene("gameplay", _gameplayScene);
        
        _sceneManager.SwitchTo("menu");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape) && 
            _sceneManager.CurrentScene?.GetType().Name == "MenuScene")
            Exit();

        _sceneManager.Update(gameTime);

        if (_menuScene.StartGameRequested)
        {
            _gameplayScene.Reset();
            _sceneManager.SwitchTo("gameplay");
            _menuScene.Reset();
        }
        
        if (_gameplayScene.ExitToMenuRequested)
        {
            _sceneManager.SwitchTo("menu");
            _menuScene.Reset();
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Coral);

        _spriteBatch.Begin();
        _sceneManager.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}