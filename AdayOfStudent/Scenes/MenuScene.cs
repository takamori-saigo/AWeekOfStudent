using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdayOfStudent.Scenes;

public class MenuScene: IScene
{
    private Game _game;
    private Texture2D _background;
    private SpriteFont _font;

    private Rectangle _buttonReact;
    private Color _buttonColor = Color.White;
    private bool _isHovering;
    
    public bool StartGameRequested { get; private set; }

    public MenuScene(Game game)
    {
        _game = game;
    }
    
    public void Initialize()
    {
        StartGameRequested = false;

        var buttonWidth = 300;
        var buttonHeight = 80;
        _buttonReact = new Rectangle(
            _game.GraphicsDevice.Viewport.Width / 2 - buttonWidth / 2,
            _game.GraphicsDevice.Viewport.Height / 2 - buttonHeight / 2,
            buttonWidth,
            buttonHeight
            );
    }

    public void LoadContent()
    {
        _font = _game.Content.Load<SpriteFont>("DefaultFont");
        _background = _game.Content.Load<Texture2D>("background");
    }

    public void Update(GameTime gameTime)
    {
        var mouse = Mouse.GetState();
        var mousePoint = new Point(mouse.X, mouse.Y);
        
        _isHovering = _buttonReact.Contains(mousePoint);
        _buttonColor = _isHovering ? Color.Red : Color.Blue;

        if (_isHovering && mouse.LeftButton == ButtonState.Pressed)
        {
            StartGameRequested = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_background, new Rectangle(0, 0, 1920, 1080), Color.White);
        spriteBatch.DrawString(_font, "START GAME",
            new Vector2(
                _buttonReact.X + (_buttonReact.Width / 2) - (_font.MeasureString("START GAME").X / 2),
                _buttonReact.Y + (_buttonReact.Height / 2) - (_font.MeasureString("START GAME").Y / 2)
            ),
            _buttonColor);
    }

    public void unloadContent()
    {
        
    }
    
    public void Reset()
    {
        StartGameRequested = false;
    }
}