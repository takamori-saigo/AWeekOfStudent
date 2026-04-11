using System;
using AdayOfStudent.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AdayOfStudent.Entities;

public static class Player
{
    public static Vector2 Position;
    public static readonly float speed = 130f;
    public static int Width;
    public static int Height;
    private static Animation _walkDownAnimation;
    private static Animation _walkUpAnimation;
    private static Animation _walkLeftAnimation;
    private static Animation _walkRightAnimation;
    private static Animation _currentAnimation;
    private static bool _isMoving;
    private static int HitboxPadding = 10; 
    public static Rectangle PlayerRectangle => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
    
    public static void Move(GameTime gameTime)
    {
        var direction = Vector2.Zero;
        
        var keyBoardState = Keyboard.GetState();    

        _isMoving = false;
        
        if (keyBoardState.IsKeyDown(Keys.W) || keyBoardState.IsKeyDown(Keys.Up))
        {
            direction.Y = -1;
            _currentAnimation = _walkUpAnimation;
            _isMoving = true;
        }
        if (keyBoardState.IsKeyDown(Keys.S) || keyBoardState.IsKeyDown(Keys.Down))
        {
            direction.Y = 1;
            _currentAnimation = _walkDownAnimation;
            _isMoving = true;
        }
        if (keyBoardState.IsKeyDown(Keys.A) || keyBoardState.IsKeyDown(Keys.Left))
        {
            direction.X = -1;
            _currentAnimation = _walkLeftAnimation;
            _isMoving = true;
        }
        if (keyBoardState.IsKeyDown(Keys.D) || keyBoardState.IsKeyDown(Keys.Right))
        {
            direction.X = 1;
            _currentAnimation = _walkRightAnimation;
            _isMoving = true;
        }
        
        if (direction.Length() > 0)
            direction.Normalize();
        
        Vector2 nextPosition = Position + direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        
        if (!CheckCollision(nextPosition.X, Position.Y))
        {
            Position.X = nextPosition.X;
        }
        
        if (!CheckCollision(Position.X, nextPosition.Y))
        {
            Position.Y = nextPosition.Y;
        }
        
        if (_isMoving)
        {
            _currentAnimation.Update(gameTime);
        }
    }

    public static void LoadAnimation(Game game)
    {
        var sprite = game.Content.Load<Texture2D>("player");
        Width = sprite.Width / 3;
        Height = sprite.Height / 4;

        _walkDownAnimation = new Animation(sprite, Width, Height, 3, 0, 0.1f, true);
        _walkUpAnimation = new Animation(sprite, Width, Height, 3, 3, 0.1f, true);
        _walkLeftAnimation = new Animation(sprite, Width, Height, 3, 1, 0.1f, true);
        _walkRightAnimation = new Animation(sprite, Width, Height, 3, 2, 0.1f, true);
        _currentAnimation = _walkDownAnimation;
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        var scale = 4.0f;
    
        var scaledWidth = (int)(Width * scale);
        var scaledHeight = (int)(Height * scale);
        Rectangle scaledRectangle = new Rectangle((int)Position.X, (int)Position.Y, scaledWidth, scaledHeight);
        if (_isMoving)
        {
            var sourceRectangle = _currentAnimation.GetCurrentFrameRectangle();
            spriteBatch.Draw(
                _currentAnimation.SpriteSheet,           
                scaledRectangle,                         
                sourceRectangle,                         
                Color.White                              
            );
        }
        else
        {
            var sourceRectangle = _currentAnimation.GetCalmStateFrame();
            spriteBatch.Draw(
                _currentAnimation.SpriteSheet,           
                scaledRectangle,                         
                sourceRectangle,                         
                Color.White                              
            );
        }
    }

    private static bool CheckCollision(float newX, float newY)
    {
        int scaledW = (int)(Width * 4);
        int scaledH = (int)(Height * 4);
        
        Rectangle hitbox = new Rectangle(
            (int)newX + HitboxPadding, 
            (int)newY + HitboxPadding, 
            scaledW - HitboxPadding * 2, 
            scaledH - HitboxPadding * 2
        );
        
        if (IsTileWallAt(hitbox.Left, hitbox.Top)) return true;
        if (IsTileWallAt(hitbox.Right, hitbox.Top)) return true;
        if (IsTileWallAt(hitbox.Left, hitbox.Bottom)) return true;
        if (IsTileWallAt(hitbox.Right, hitbox.Bottom)) return true;

        return false;
    }
    
    private static bool IsTileWallAt(int pixelX, int pixelY)
    {
        var tileX = pixelX / Map.Size;
        int tileY = pixelY / Map.Size;
        return Map.IsWall(tileX, tileY);
    }
}