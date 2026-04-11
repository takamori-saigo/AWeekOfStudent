    using System;
    using AdayOfStudent.Entities;
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
            Player.Position = new Vector2(_game.GraphicsDevice.Viewport.Width / 2 - 53, _game.GraphicsDevice.Viewport.Height / 2 + 150);
        }

        public void LoadContent()
        {
            Player.LoadAnimation(_game);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ExitToMenuRequested = true;
            }

            Console.WriteLine(Player.Position);
            Player.Move(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
        }

        public void Reset()
        {
            Initialize();
        }
        
        public void unloadContent()
        {
            
        }
    }