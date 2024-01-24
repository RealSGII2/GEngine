namespace GEngine.Engine.Graphics;

using Silk.NET.OpenGL;
using Silk.NET.Windowing;

public class GraphicsController
{
    /// <summary>
    /// The currently used OpenGL instance.
    /// </summary>
    public GL OpenGL {
        get => _internalGL!;
    }

    /// <summary>
    /// Whether graphics are ready to be used.
    /// </summary>
    public bool IsLoaded {
        get => _internalGL is not null;
    }
    
    /// <summary>
    /// Fired when graphics are ready to be used.
    /// </summary>
    public event Action? Ready;

    private GL? _internalGL;

    private readonly IWindow _window;
    private readonly List<RenderedObject> _objects = new();

    // This should be called when the window is loaded.
    public GraphicsController(IWindow window)
    {
        _window = window;
        _window.Load += OnLoad;
        _window.Render += OnRender;
    }

    /// <summary>
    /// Adds a RenderedObject to the render stack.
    /// </summary>
    /// <param name="obj">The object to render.</param>
    public void AddObject(RenderedObject obj)
    {
        _objects.Add(obj);
    }

    private void OnLoad()
    {
        _internalGL = _window.CreateOpenGL();

        Action? ready = Ready;
        
        if (ready is not null)
            ready();
    }

    private unsafe void OnRender(double deltaTime)
    {
        if (!IsLoaded) return;

        // Do background blits.
        OpenGL.Clear(ClearBufferMask.ColorBufferBit);

        // Render everything in the render stack.
        foreach (var obj in _objects)
        {
            OpenGL.BindVertexArray(obj.VertexArray);
            OpenGL.UseProgram(obj.ShaderProgram);
            OpenGL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, (void*) 0);
        }
    }
}
