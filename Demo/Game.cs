namespace GEngine.Demo;

using GEngine.Engine.Graphics;
using System.Drawing;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

public class Game
{
    public readonly IWindow GameWindow;
    private readonly WindowOptions _windowOptions = WindowOptions.Default with
    {
        Size = new Vector2D<int>(800, 600),
        Title = "Demo"
    };

    private readonly GraphicsController _graphics;

    public Game()
    {
        // Create the window.
        GameWindow = Window.Create(_windowOptions);
        GameWindow.Load += OnLoad;
        
        // Start graphics
        _graphics = new(GameWindow);
        _graphics.Ready += OnGraphicsReady;

        // Start the game
        GameWindow.Run();
    }

    private void OnGraphicsReady()
    {
        // Render our graphics.
        _graphics!.OpenGL.ClearColor(Color.Wheat);

        using GeometryRenderer renderer = new(_graphics!);
        renderer.RenderGeometry(Meshes.BigRectangle);
        renderer.RenderGeometry(Meshes.SmallRectangle);
    }

    private void OnLoad()
    {
        // Input
        IInputContext input = GameWindow.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++)
            input.Keyboards[i].KeyDown += OnKeyDown;
    }

    private void OnKeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        if (key == Key.Escape)
            GameWindow.Close();
    }
}
