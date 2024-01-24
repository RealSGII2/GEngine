using System.Xml;
using GEngine.Engine.Assets.Extensions;

namespace GEngine.Engine.Assets.AssetTypes;

public record Head : IAsset
{
    public required string Version { get; init; }

    public static IAsset FromNode(XmlNode node)
    {
        return new Head()
        {
            Version = node.GetAttributeOrError("v")
        };
    }
}
