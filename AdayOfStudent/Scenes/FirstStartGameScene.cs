using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace AdayOfStudent.Scenes;

public class FirstStartGameScene: IScene
{
    private Texture2D _background;
    private Game _game;
    private float _alpha = 0f;
    private float _fadeSpeed = 0.4f;
    private bool _isFading = true;
    private bool _isComplete;
    private bool _isKeyWasPressed;
    
    public bool StartTheGame
    {
        get
        {
            return _isKeyWasPressed;
        }
        set{}
    }

    public FirstStartGameScene(Game game)
    {
        _game = game;
    }
    
    public void Initialize()
    {
        
    }

    public void LoadContent()
    {
        _background = _game.Content.Load<Texture2D>("predislovie");
    }

    public void Update(GameTime gameTime)
    {

        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (_isFading)
        {
            _alpha += deltaTime * _fadeSpeed;
            if (_alpha >= 1)
            {
                _isComplete = true;
            }
        }

        if (_isComplete)
        {
            var keyboardState = Keyboard.GetState();
            var mouse = Mouse.GetState();
            if (keyboardState.GetPressedKeyCount() > 0 || 
                mouse.RightButton == ButtonState.Pressed ||
                mouse.LeftButton == ButtonState.Pressed)
            {
                _isKeyWasPressed = true;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
       spriteBatch.Draw(_background,new Rectangle( 0,0, 1600, 900), Color.White * _alpha);
    }

    public void unloadContent()
    {
        
    }
}