    using System;
    using AdayOfStudent.Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    namespace AdayOfStudent.Scenes;

    public class GameplayScene: IScene
    {
        private Game _game;
        private Vector2 _playerStartPosition = new Vector2(-53, 150);
        public bool ExitToMenuRequested { get; private set; }
        private Camera _camera;

        public GameplayScene(Game game)
        {
            _game = game;
        }
        
        public void Initialize()
        {
            ExitToMenuRequested = false;
            Player.Position = _playerStartPosition;
            Map.SetGame(_game);
            _camera = new Camera(_game.GraphicsDevice.Viewport);
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
            Player.Move(gameTime);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: _camera.GetTransformation());
            Map.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void Reset()
        {
            Initialize();
        }
        
        public void unloadContent()
        {
            
        }
    }