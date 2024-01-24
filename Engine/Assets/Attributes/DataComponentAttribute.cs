namespace GEngine.Engine.Assets.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class DataComponentAttribute : Attribute
{
    public readonly string TagName;

    public DataComponentAttribute(string tagName)
    {
        TagName = tagName;
    }
}