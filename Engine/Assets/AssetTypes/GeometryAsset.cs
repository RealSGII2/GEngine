using System.Xml;
using GEngine.Engine.Assets.Attributes;
using GEngine.Engine.Assets.Extensions;
using GEngine.Engine.Graphics;

namespace GEngine.Engine.Assets.AssetTypes;

[DataComponent("Geometry")]
public record GeometryAsset : IAsset
{
    public required string BoundMeshPath { get; init; }

    public required string VertexScriptPath  { get; init; }
    public required string FragScriptPath { get; init; }

    public MeshData Mesh
    {
        get => new() {
            Vertices = Array.Empty<float>(),
            Indices = Array.Empty<uint>()
        };
    }

    public string VertexScript
    {
        get => "";
    }

    public string FragScript
    {
        get => "";
    }

    public static IAsset FromNode(XmlNode node)
    {
        return new GeometryAsset()
        {
            BoundMeshPath = node.GetAttributeOrError("bind"),

            VertexScriptPath = node
                .GetChildByTagNameOrError("VertexScript")
                .GetAttributeOrError("source"),

            FragScriptPath = node
                .GetChildByTagNameOrError("FragScript")
                .GetAttributeOrError("source")
        };
    }
}
