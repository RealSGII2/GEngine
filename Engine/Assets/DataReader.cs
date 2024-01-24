using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using GEngine.Engine.Assets.AssetTypes;
using GEngine.Engine.Assets.Attributes;

namespace GEngine.Engine.Assets;

public class DataReader<T> where T : IAsset
{
    public readonly Head Head;
    public readonly T Body;

    public DataReader(string path)
    {
        var document = new XmlDocument();
        document.Load(path);

        // Get the header
        var headElements = document.GetElementsByTagName("Head");
        if (headElements.Count > 0 && headElements[0] is not null)
            Head = (Head)Head.FromNode(headElements[0]!);
        else
            throw new Exception("Head was not found in Asset data.");

        // Get the body
        var attribute = (DataComponentAttribute?)
            typeof(T).GetCustomAttribute(typeof(DataComponentAttribute), true);

        if (attribute is not null)
        {
            var elements = document.GetElementsByTagName(attribute.TagName);

            if (elements.Count > 0 && elements[0] is not null)
            {
                Body = (T)T.FromNode(elements[0]!);
            }
        }

        if (Body is null)
            throw new Exception($"{nameof(T)} was not found in Asset '{path}'");
    }
}
