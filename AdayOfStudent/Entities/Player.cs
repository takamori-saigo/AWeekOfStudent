using AdayOfStudent.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AdayOfStudent.Entities;

public static class Player
{
    public static Vector2 Position;
    public static readonly float speed = 15f;
    public static int Width;
    public static int Height;
    
    private static Animation _idleAnimation;
    private static Animation _walkDownAnimation;
    private static Animation _walkUpAnimation;
    private static Animation _walkLeftAnimation;
    private static Animation _walkRightAnimation;
    
    private static Animation _currentAnimation;
    private static bool _isMoving;
    
    public static Rectangle PlayerRectangle => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

    public static void LoadAnimations(Game game)
    {
        var spriteSheet = game.Content.Load<Texture2D>("Player");
    }
    
    public static void Move()
    {
        var x = 0;
        var y = 0;
        if (Keyboard.GetState().IsKeyDown(Keys.W) ||
            Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            y = -1;
        }if (Keyboard.GetState().IsKeyDown(Keys.S) ||
            Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            y = 1;
        }if (Keyboard.GetState().IsKeyDown(Keys.A) ||
            Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            x = -1;
        }if (Keyboard.GetState().IsKeyDown(Keys.D) ||
            Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            x = 1;
        }
        
        var vectorVelocity = new Vector2(x, y) * speed;
        Position.X += (int)vectorVelocity.X;
        Position.Y += (int)vectorVelocity.Y;
    }
}