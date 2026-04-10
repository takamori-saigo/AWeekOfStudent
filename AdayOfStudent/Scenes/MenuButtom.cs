using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdayOfStudent.Scenes;

public class MenuButtom
{
    public Rectangle Rectangle { get; private set; }
    public Color Color { get ; private set; }
    public bool IsHovering { get; private set; }
    
    public MenuButtom(Rectangle rectangle, Color color)
    {
        Rectangle = rectangle;
        Color = color * 0.8f;
        IsHovering = false;
    }

    public void CheckPositionMouse(Point positionMouse)
    {
        bool wasHovering = IsHovering;
        IsHovering = Rectangle.Contains(positionMouse);
        
        if (wasHovering != IsHovering)
        {
            UpdateColor();
        }
    }
    
    public void DrawRectangleOutline(SpriteBatch spriteBatch,
        Color color, Texture2D _pixel, int thickness = 2)
    {
        spriteBatch.Draw(_pixel, new Rectangle(Rectangle.X, Rectangle.Y, Rectangle.Width, thickness), color);
        spriteBatch.Draw(_pixel, new Rectangle(Rectangle.X, Rectangle.Y + Rectangle.Height - thickness, Rectangle.Width, thickness), color);
        spriteBatch.Draw(_pixel, new Rectangle(Rectangle.X, Rectangle.Y, thickness, Rectangle.Height), color);
        spriteBatch.Draw(_pixel, new Rectangle(Rectangle.X + Rectangle.Width - thickness, Rectangle.Y, thickness, Rectangle.Height), color);
    }
    
    private void UpdateColor()
    {
        if (IsHovering)
        {
            Color = new Color(Color, 0.8f); 
        }
        else
        {
            Color = new Color(Color, 0.5f); 
        }
    }
}