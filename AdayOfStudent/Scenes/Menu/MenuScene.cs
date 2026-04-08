using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdayOfStudent.Scenes;

public class MenuScene: IScene
{
    private Game _game;
    private Texture2D _background;
    private Texture2D _pixel;   
    private SpriteFont _font;
    
    private MenuButtom _buttomStart;
    private MenuButtom _buttomSettings;
    
    private Color _buttonColor = Color.White;
    
    public bool StartGameRequested { get; private set; }

    public MenuScene(Game game)
    {
        _game = game;
    }
    
    public void Initialize()
    {
        StartGameRequested = false;

        var buttonWidth = 300;
        var buttonHeight = 70;

        _buttomStart = new MenuButtom(new Rectangle(
            _game.GraphicsDevice.Viewport.Width / 2 - buttonWidth / 2,
            _game.GraphicsDevice.Viewport.Height / 2 - buttonHeight / 2,
            buttonWidth,
            buttonHeight
        ), _buttonColor);
        
        _buttomSettings = new MenuButtom(new Rectangle(
            _game.GraphicsDevice.Viewport.Width / 2 - buttonWidth / 2,
            _game.GraphicsDevice.Viewport.Height / 2 - buttonHeight / 2 + 100,
            buttonWidth,
            buttonHeight
        ), _buttonColor);
        
        _pixel = new Texture2D(_game.GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
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
        
        _buttomStart.CheckPositionMouse(mousePoint);
        _buttomSettings.CheckPositionMouse(mousePoint);
        
        if (_buttomStart.IsHovering && mouse.LeftButton == ButtonState.Pressed)
        {
            StartGameRequested = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_background, new Rectangle(0, 0, 1920, 1080), Color.White);
       
        spriteBatch.Draw(_pixel, _buttomStart.Rectangle, _buttomStart.Color);
        
        spriteBatch.Draw(_pixel, _buttomSettings.Rectangle, _buttomStart.Color);

        
        _buttomStart.DrawRectangleOutline(spriteBatch, _buttonColor, _pixel);
        _buttomSettings.DrawRectangleOutline(spriteBatch, _buttonColor, _pixel);
        
        spriteBatch.DrawString(_font, "START GAME",
            new Vector2(
                _buttomStart.Rectangle.X + ( _buttomStart.Rectangle.Width / 2) - (_font.MeasureString("START GAME").X / 2),
                _buttomStart.Rectangle.Y + ( _buttomStart.Rectangle.Height / 2) - (_font.MeasureString("START GAME").Y / 2)
            ),
            Color.Black);
        
        spriteBatch.DrawString(_font, "SETTINGS",
            new Vector2(
                _buttomSettings.Rectangle.X + ( _buttomStart.Rectangle.Width / 2) - (_font.MeasureString("SETTINGS").X / 2),
                _buttomSettings.Rectangle.Y + ( _buttomSettings.Rectangle.Height / 2) - (_font.MeasureString("SETTINGS").Y / 2)
            ),
            Color.Black);
    }

    public void unloadContent()
    {
        
    }
    
    public void Reset()
    {
        StartGameRequested = false;
    }
}