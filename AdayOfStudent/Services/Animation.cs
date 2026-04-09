using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdayOfStudent.Scenes;

public class Animation
{
    public Texture2D SpriteSheet { get; set; }
    public int FrameWidth { get; set; }
    public int FrameHeight { get; set; }
    public int FrameCount { get; set; }
    public int Row { get; set; }
    public float FrameTime { get; set; }
    public bool IsLooping { get; set; }

    private int _currentFrame;
    private float _timeAccumulator;
    
    public Animation(Texture2D spriteSheet, int frameWidth, int frameHeight, 
        int frameCount, int row = 0, float frameTime = 0.1f, bool isLooping = true)
    {
        SpriteSheet = spriteSheet;
        FrameWidth = frameWidth;
        FrameHeight = frameHeight;
        FrameCount = frameCount;
        Row = row;
        FrameTime = frameTime;
        IsLooping = isLooping;
        _currentFrame = 0;
        _timeAccumulator = 0;
    }

    public void Update(GameTime gameTime)
    {
        _timeAccumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timeAccumulator >= FrameTime)
        {
            _timeAccumulator = 0;
            _currentFrame++;

            if (_currentFrame >= FrameCount)
            {
                if (IsLooping)
                {
                    _currentFrame = 0;
                }
                else
                {
                    _currentFrame = FrameCount - 1;
                }
            }
        }
    }

    public Rectangle GetCurrentFrameRectangle()
    {
        return new Rectangle(_currentFrame * FrameWidth, Row * FrameHeight, FrameWidth, FrameHeight);
    }

    public void Reset()
    {
        _currentFrame = 0;
        _timeAccumulator = 0;
    }
}