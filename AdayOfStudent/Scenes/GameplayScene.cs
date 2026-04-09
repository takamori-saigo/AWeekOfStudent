    using AdayOfStudent.Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    namespace AdayOfStudent.Scenes;

    public class GameplayScene: IScene
    {
        private Game _game;
        private Texture2D _pixel;

        public bool ExitToMenuRequested { get; private set; }
        
        public GameplayScene(Game game)
        {
            _game = game;
        }
        
        public void Initialize()
        {
            Player.PlayerRectangle = new Rectangle(_game.GraphicsDevice.Viewport.Width / 2 - 300 / 2,
            _game.GraphicsDevice.Viewport.Height / 2 - 70 / 2, 300, 70);
            
            ExitToMenuRequested = false;
            _pixel = new Texture2D(_game.GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.Purple });
        }

        public void LoadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ExitToMenuRequested = true;
            }
            
            Player.Move();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_pixel, Player.PlayerRectangle, Color.Red);
        }

        public void Reset()
        {
            Initialize();
        }
        
        public void unloadContent()
        {
            
        }
    }