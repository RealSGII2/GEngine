namespace GEngine.Engine.Assets.AssetTypes;

using System.Xml;

public interface IAsset
{
    public static abstract IAsset FromNode(XmlNode node);
}
