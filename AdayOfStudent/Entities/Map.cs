using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdayOfStudent.Entities;

public static class Map
{
    private static string[,] levelMap = new string[,]
    {
        {"#", "#", "#", "#", "#", "#", "#", "#", "#", "#"},
        {"#", "*", "*", "*", "#", "*", "*", "*", "*", "#"},
        {"#", "*", "#", "*", "#", "*", "#", "#", "*", "#"},
        {"#", "*", "#", "*", "*", "*", "*", "#", "*", "#"},
        {"#", "*", "#", "#", "#", "#", "*", "#", "*", "#"},
        {"#", "*", "*", "*", "*", "#", "*", "*", "*", "#"},
        {"#", "#", "#", "#", "*", "#", "*", "#", "#", "#"},
        {"#", "*", "*", "*", "*", "*", "*", "*", "*", "#"},
        {"#", "*", "#", "#", "#", "#", "#", "#", "*", "#"},
        {"#", "#", "#", "#", "#", "#", "#", "#", "#", "#"}
    };
    
    private static Texture2D _pixel;
    private static Game _game;
    public static int Size = 37;
    
    public static bool IsWall(int x, int y)
    {
        if (y < 0 || y >= levelMap.GetLength(0) || x < 0 || x >= levelMap.GetLength(1))
            return true; 

        return levelMap[y, x] == "#";
    }
    
    public static void SetGame(Game game)
    {
        _game = game;
        _pixel = new Texture2D(_game.GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
    }
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        for (var x = 0; x < levelMap.GetLength(0); x++)
        {
            for (var y = 0; y < levelMap.GetLength(1); y++)
            {
                if (levelMap[x, y] == "#")
                {
                    var screenX = x * Size;
                    var screenY = y * Size;
                    var tile = new Rectangle(screenX, screenY, Size - 3, Size - 3);
                    spriteBatch.Draw(_pixel, tile, Color.Gray);
                }
            }
        }        
    }
}