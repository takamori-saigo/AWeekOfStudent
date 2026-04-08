using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdayOfStudent.Scenes;

public class FirstStartGameScene: IScene
{
    private Texture2D _background;
    private Game _game;
    
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
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
       spriteBatch.Draw(_background,new Rectangle( 0,0, 1600, 900), Color.White);
    }

    public void unloadContent()
    {
        
    }
}