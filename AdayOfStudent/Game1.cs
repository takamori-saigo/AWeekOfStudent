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
    private bool isFirstStart = true;
    private MenuScene _menuScene;
    private GameplayScene _gameplayScene;
    private FirstStartGameScene _firstStartGameScene;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.IsFullScreen = false; 
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _sceneManager = new SceneManager();
        
        _menuScene = new MenuScene(this);
        _gameplayScene = new GameplayScene(this);
        _firstStartGameScene = new FirstStartGameScene(this);
        
        _sceneManager.AddScene("menu", _menuScene);
        _sceneManager.AddScene("gameplay", _gameplayScene);
        _sceneManager.AddScene("predislovie", _firstStartGameScene);
        
        _sceneManager.SwitchTo("gameplay");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        _sceneManager.Update(gameTime);
        
        if (_menuScene.StartGameRequested)
        {
            _sceneManager.SwitchTo("predislovie");
            _menuScene.Reset();
        }

        if (_firstStartGameScene.StartTheGame)
        {
            _sceneManager.SwitchTo("gameplay");
            _firstStartGameScene.StartTheGame = false; 
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightPink);

        _spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,  
            blendState: BlendState.AlphaBlend,
            rasterizerState: RasterizerState.CullNone
        );
        _sceneManager.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}