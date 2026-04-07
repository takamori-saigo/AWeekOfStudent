using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdayOfStudent.Scenes;

public class SceneManager
{
    private Dictionary<string, IScene> _scenes = new();
    
    public IScene CurrentScene { get; private set; }

    public void AddScene(string name, IScene scene)
    {
        _scenes.Add(name, scene);
    }

    public void SwitchTo(string name)
    {
        CurrentScene?.unloadContent();
        CurrentScene = _scenes[name];
        CurrentScene.Initialize();
        CurrentScene.LoadContent();
    }

    public void Update(GameTime gameTime)
    {
        CurrentScene?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        CurrentScene?.Draw(spriteBatch);
    }
}