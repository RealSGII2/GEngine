namespace GEngine.Engine.Graphics;

/// <summary>
/// Represents an object registered with OpenGL.
/// </summary>
public record RenderedObject
{
    public required uint VertexArray { get; init; }
    public required uint ShaderProgram { get; init; }
}
