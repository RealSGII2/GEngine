using System.Xml;

namespace GEngine.Engine.Assets.Exceptions;

public class NullAttributeException : XmlException
{
    public NullAttributeException(string attributeName)
        : base($"Attribute collection or attribute {attributeName} is null.")
    {
        Data.Add("AttributeName", attributeName);
    }
}
