using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdayOfStudent.Entities;

public class Camera
{
    public Vector2 Position { get; set; }
    private Viewport _viewport;

    public Camera(Viewport viewport)
    {
        _viewport = viewport;
        Position = Vector2.Zero;
    }

    public Matrix GetTransformation()
    {
        return Matrix.CreateTranslation(new Vector3(-Position, 0));
    }

    public void Update(Vector2 target, Rectangle map)
    {
       var newPos = new Vector2(target.X - _viewport.Width / 2,
           target.Y - _viewport.Height / 2);
        
       if (Position.X < 0) newPos.X = 0;
       if (Position.Y < 0) newPos.Y = 0;
        
       if (Position.X + _viewport.Width > map.Width)
           newPos.X = map.Width - _viewport.Width;
       
       if (Position.Y + _viewport.Height > map.Height)
           target.Y = map.Height - _viewport.Height;
       
       if (map.Width < _viewport.Width) target     .X = -( _viewport.Width - map.Width) / 2;
       if (map.Height < _viewport.Height) target.Y = -( _viewport.Height - map.Height) / 2;
       
       Position = newPos;
    }
}