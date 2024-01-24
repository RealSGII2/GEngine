namespace GEngine.Engine.Graphics;

/// <summary>
/// A 3D object.
/// </summary>
public record Geometry
{
    /// <summary>
    /// The physical composition of this object.
    /// </summary>
    public required MeshData Mesh { get; init; }

    /// <summary>
    /// A script that handles the processing of vertices.
    /// </summary>
    public required string VertexScript { get; init; }

    /// <summary>
    /// A script that generates a Fragment for this Geometry.
    /// </summary>
    public required string FragScript { get; init; }
}
