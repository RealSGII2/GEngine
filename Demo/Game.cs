namespace GEngine.Demo;

using GEngine.Engine.Graphics;
using System.Drawing;
using Silk.NET.Input;
using GEngine.Engine;

public class MainGame
{
    public readonly Game Game;

    public MainGame()
    {
        Game = new();
        Game.KeyDown += OnKeyDown;
        Game.Graphics.Ready += OnGraphicsReady;

        Game.Start();
    }

    private void OnGraphicsReady()
    {
        // Render our graphics.
        Game.Graphics.OpenGL.ClearColor(Color.Wheat);

        // TODO: Transition GeometryRenderer to use GeometryAsset
        //       instead of Geometry (so we can remove Demo.Old)
        using GeometryRenderer renderer = new(Game.Graphics);
        renderer.RenderGeometry(Old.Meshes.BigRectangle);
        renderer.RenderGeometry(Old.Meshes.SmallRectangle);
    }

    private void OnKeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        switch(key)
        {
            case Key.Escape:
                Game.Exit();
                break;
        }
    }
}
