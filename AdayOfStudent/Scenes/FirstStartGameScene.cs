using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdayOfStudent.Scenes;

public class FirstStartGameScene: IScene
{
    private Texture2D _background;
    private Game _game;
    private float _alpha = 0f;
    private float _fadeSpeed = 0.4f;
    private bool _isFading = true;
    private bool _isComplete = false;
    
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
        if (_isComplete) return;
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (_isFading)
        {
            _alpha += deltaTime * _fadeSpeed;
            if (_alpha >= 1)
            {
                _isComplete = true;
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