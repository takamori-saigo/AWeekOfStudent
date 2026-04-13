using System;
using AdayOfStudent.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AdayOfStudent.Entities;

public static class Player
{
    public static Vector2 Position;
    public static readonly float speed = 300f;
    public static int Width;
    public static int Height;
    private static Animation _walkDownAnimation;
    private static Animation _walkUpAnimation;
    private static Animation _walkLeftAnimation;
    private static Animation _walkRightAnimation;
    private static Animation _currentAnimation;
    private static float _scaleOfDrawing = 4.0f;
    private static bool _isMoving;
    private static int HitboxOffsetY;

    private static Texture2D _hitboxTextureDebug;
    
    public static void LoadAnimation(Game game)
    {
        var sprite = game.Content.Load<Texture2D>("player");
        
        _hitboxTextureDebug = new Texture2D(game.GraphicsDevice, 1, 1);
        _hitboxTextureDebug.SetData(new [] { SharpDX.Color.Red });
        
        Width = sprite.Width / 3;
        Height = sprite.Height / 4;
        _walkDownAnimation = new Animation(sprite, Width, Height, 3, 0, 0.1f, true);
        _walkUpAnimation = new Animation(sprite, Width, Height, 3, 3, 0.1f, true);
        _walkLeftAnimation = new Animation(sprite, Width, Height, 3, 1, 0.1f, true);
        _walkRightAnimation = new Animation(sprite, Width, Height, 3, 2, 0.1f, true);
        _currentAnimation = _walkDownAnimation;
        HitboxOffsetY = (int)(Height * _scaleOfDrawing * 0.35f);
    }
    
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
        
        var nextPosition = Position + direction * speed * 
            (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (CheckCollision(nextPosition.X, nextPosition.Y))
            Position = nextPosition;
                
        if (_isMoving)
            _currentAnimation.Update(gameTime);
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        var scaledWidth = (int)(Width * _scaleOfDrawing);
        var scaledHeight = (int)(Height * _scaleOfDrawing);
        var scaledRectangle = new Rectangle((int)Position.X, (int)Position.Y, scaledWidth, scaledHeight);
        var sourceRectangle = _isMoving ? _currentAnimation.GetCurrentFrameRectangle()
            : _currentAnimation.GetCalmStateFrame();
        spriteBatch.Draw(
            _currentAnimation.SpriteSheet,           
            scaledRectangle,                         
            sourceRectangle,                         
            Color.White                              
        );
        //for debbugging hitboxes   
        /*var scaledW = (int)(Width * _scaleOfDrawing);
        var scaledH = (int)(Height * _scaleOfDrawing);
        var hitbox = new Rectangle(
            (int)Position.X,
            (int)Position.Y + HitboxOffsetY,
            scaledW,
            scaledH - HitboxHeightPadding
        );
        spriteBatch.Draw(_hitboxTextureDebug, hitbox, Color.Red * 0.5f);*/
    }

    private static bool CheckCollision(float newX, float newY)
    {
        var scaledW = (int)(Width * _scaleOfDrawing);
        var scaledH = (int)(Height * _scaleOfDrawing);

        var hitbox = new Rectangle((int)newX, (int)newY+HitboxOffsetY,
            scaledW, scaledH - HitboxOffsetY);
        
        if (IsTileWallAt(hitbox.Left, hitbox.Top)) return false;
        if (IsTileWallAt(hitbox.Right, hitbox.Top)) return false;
        if (IsTileWallAt(hitbox.Left, hitbox.Bottom)) return false;
        if (IsTileWallAt(hitbox.Right, hitbox.Bottom)) return false;
        

        return true;
    }
    
    private static bool IsTileWallAt(int pixelX, int pixelY)
    {
        var tileX = pixelX / Map.SizeOfTile;
        int tileY = pixelY / Map.SizeOfTile;
        return Map.IsSolid(tileX, tileY);
    }
}