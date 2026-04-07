using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdayOfStudent.Scenes;

public class GameplayScene: IScene
{
    private Game _game;
    
    public bool ExitToMenuRequested { get; private set; }
    
    public GameplayScene(Game game)
    {
        _game = game;
    }
    
    public void Initialize()
    {
        ExitToMenuRequested = false;
    }

    public void LoadContent()
    {
        
    }

    public void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            ExitToMenuRequested = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        
    }

    public void Reset()
    {
        Initialize();
    }
    
    public void unloadContent()
    {
        throw new System.NotImplementedException();
    }
}