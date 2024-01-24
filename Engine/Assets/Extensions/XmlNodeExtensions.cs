using System.Linq;
using System.Xml;
using GEngine.Engine.Assets.Exceptions;
using Silk.NET.Core;

namespace GEngine.Engine.Assets.Extensions;

public static class XmlNodeExtensions
{
    public static XmlNode GetChildByTagNameOrError(this XmlNode node, string tagName)
    {
        return ((IEnumerable<XmlNode>)node.ChildNodes).First(x => x.LocalName == tagName);
    }

    public static string GetAttributeOrError(this XmlNode node, string name)
    {
        var result = (node.Attributes?[name]) ?? throw new NullAttributeException(name);
        return result.Value;
    }
}
