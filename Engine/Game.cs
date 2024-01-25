using GEngine.Engine.Graphics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace GEngine.Engine;

public class Game
{
    // Parameters
    public Vector2D<int> WindowSize = new(800, 800);

    // Events
    public event Action? Loaded;
    public event Action<double>? Render;
    public event Action<IKeyboard, Key, int>? KeyDown;

    // Objects
    public GraphicsController Graphics;
    
    // Internal state
    private IWindow _window;

    public Game()
    {
        _window = Window.Create(new()
        {
            Size = WindowSize,
            Title = "Application"
        });

        Graphics = new(_window);

        _window.Load += WindowLoaded;
        _window.Render += WindowRender;
    }

    public void Start()
    {
        _window.Run();
    }

    public void Exit()
    {
        _window.Close();
    }

    private void WindowLoaded()
    {
        // Fire Loaded event
        var loadedEvent = Loaded;
        
        if (loadedEvent is not null)
            loadedEvent();

        // Register Inputs
        IInputContext input = _window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++)
            input.Keyboards[i].KeyDown += WindowKeyDown;
    }
    private void WindowRender(double deltaTime)
    {
        var renderEvent = Render;
        
        if (renderEvent is not null)
            renderEvent(deltaTime);
    }

    private void WindowKeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        var keyDownEvent = KeyDown;

        if (keyDownEvent is not null)
            keyDownEvent(keyboard, key, keyCode);
    }
}
