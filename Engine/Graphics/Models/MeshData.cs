namespace GEngine.Engine.Graphics;

/// <summary>
/// The physical composition of a 3D object.
/// </summary>
public record MeshData
{
    public required float[] Vertices { get; init; }
    public required uint[] Indices { get; init; }
}
