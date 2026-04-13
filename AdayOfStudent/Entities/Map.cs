using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdayOfStudent.Entities;

public static class Map
{
    private static string[,] test;
    
    private static Texture2D _pixel;
    private static Game _game;
    public static int SizeOfTile = 20;

    public static bool IsSolid(int x, int y)
    {
        if (x < 0 || x >= test.GetLength(0) || y < 0 || y >= test.GetLength(1))
            return true;
        return test[x, y] == "#";
    }
    
    public static void InitializeMap(Game game)
    {
        _game = game;
        _pixel = new Texture2D(_game.GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
       
        test = new string[_game.GraphicsDevice.Viewport.Width/SizeOfTile, _game.GraphicsDevice.Viewport.Height/SizeOfTile];
        int cols = _game.GraphicsDevice.Viewport.Width / SizeOfTile;
        int rows = _game.GraphicsDevice.Viewport.Height / SizeOfTile;

        test = new string[cols, rows];
        
        for (var i = 0; i < cols; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                if (i == 0 || i == cols - 1 || j == 0 || j == rows - 1)
                    test[i, j] = "#";
                else 
                    test[i, j] = "*";
            }
        }
    }
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        for (var x = 0; x < test.GetLength(0); x++)
        {
            for (var y = 0; y < test.GetLength(1); y++)
            {
                if (test[x, y] == "#")
                {
                    var screenX = x * SizeOfTile;
                    var screenY = y * SizeOfTile;
                    var tile = new Rectangle(screenX, screenY, SizeOfTile, SizeOfTile);
                    spriteBatch.Draw(_pixel, tile, Color.Gray);
                }
            }
        } 
    }
}