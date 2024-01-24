using Silk.NET.OpenGL;

namespace GEngine.Engine.Graphics;

public class GeometryRenderer : IDisposable
{
    /// <summary>
    /// The graphics this Loader loads to.
    /// </summary>
    public readonly GraphicsController Graphics;

    /// <summary>
    /// A shorthand for Graphics.OpenGL.
    /// </summary>
    private GL GL {
        get => Graphics.OpenGL;
    }

    /// <summary>
    /// A list of shaders created by this Loader.
    /// Any shader added here will be deleted from memory when 
    /// disposed.
    /// </summary>
    private readonly List<GeometryShader> _shaders = new();

    /// <summary>
    /// Describes whether this instance is disposed or not.
    /// </summary>
    private bool _isDisposed = false;

    public GeometryRenderer(GraphicsController graphics)
    {
        Graphics = graphics;
    }

    public unsafe void RenderGeometry(Geometry geometry)
    {
        // The shader program.
        uint program = GL.CreateProgram();

        // Init mesh composition.
        uint vertexArray = GL.GenVertexArray();
        GL.BindVertexArray(vertexArray);

        uint vertexBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTargetARB.ArrayBuffer, vertexBuffer);

        uint elementBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, elementBuffer);

        // Fill mesh composition data
        fixed (float* vertices = geometry.Mesh.Vertices)
        GL.BufferData(
            BufferTargetARB.ArrayBuffer,
            (nuint) (geometry.Mesh.Vertices.Length * sizeof(float)),
            vertices,
            BufferUsageARB.StaticDraw
        );

        fixed (uint* buf = geometry.Mesh.Indices)
        GL.BufferData(
            BufferTargetARB.ElementArrayBuffer,
            (nuint) (geometry.Mesh.Indices.Length * sizeof(uint)),
            buf,
            BufferUsageARB.StaticDraw
        );

        // Create and use our shaders.
        CreateShader(program, ShaderType.VertexShader, geometry.VertexScript);
        CreateShader(program, ShaderType.FragmentShader, geometry.FragScript);

        GL.LinkProgram(program);

        // Check if shaders worked.
        GL.GetProgram(program, ProgramPropertyARB.LinkStatus, out int shaderStatus);
        if (shaderStatus != (int)GLEnum.True)
            throw new Exception(
                "Shaders failed to compile: " + GL.GetProgramInfoLog(program)
            );

        // Register the object with the graphics controller.
        Graphics.AddObject(new()
        {
            VertexArray = vertexArray,
            ShaderProgram = program
        });

        // Position the object.
        var positionLoc = (uint)GL.GetAttribLocation(program, "aPosition");
        GL.EnableVertexAttribArray(positionLoc);
        GL.VertexAttribPointer(
            positionLoc,
            3,
            VertexAttribPointerType.Float,
            false,
            3 * sizeof(float),
            (void*) 0
        );

        // Finish up: clear memory
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
    }

    /// <summary>
    /// Creates a shader in OpenGL's state.
    /// </summary>
    /// <param name="program">The program to write the shader to.</param>
    /// <param name="type">The type of shader to create.</param>
    /// <param name="script">The source of the shader.</param>
    /// <returns>The OpenGL reference of this shader.</returns>
    /// <exception cref="Exception">The shader failed to compile.</exception>
    private uint CreateShader(uint program, ShaderType type, string script)
    {
        // Create a shader with a script, and compile it.
        uint shader = GL.CreateShader(type);
        GL.ShaderSource(shader, script);
        GL.CompileShader(shader);

        // Check if the shader was created successfully.
        GL.GetShader(shader, ShaderParameterName.CompileStatus, out int status);
        if (status != (int)GLEnum.True)
            throw new Exception(
                "Shader failed to compile: " + GL.GetShaderInfoLog(shader)
            );

        _shaders.Add(new GeometryShader()
        {
            Shader = shader,
            Program = program
        });

        GL.AttachShader(program, shader);

        return shader;
    }

    /// <summary>
    /// Deletes a shader from state.
    /// </summary>
    /// <param name="program">The program containing the shader.</param>
    /// <param name="shader">The reference of the shader.</param>
    private void DeleteShader(uint program, uint shader)
    {
        GL.DetachShader(program, shader);
        GL.DeleteShader(shader);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            foreach (var shader in _shaders)
                DeleteShader(shader.Program, shader.Shader);
            
            _isDisposed = true;
        }
    }

    ~GeometryRenderer()
        => Dispose(disposing: false);

    /// <summary>
    /// Dispose of this Loader.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

internal record GeometryShader
{
    public required uint Program { get; init; }
    public required uint Shader { get; init; }
}
