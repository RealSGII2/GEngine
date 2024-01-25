using GEngine.Engine.Assets;
using GEngine.Engine.Assets.AssetTypes;
using GEngine.Engine.Graphics;

namespace GEngine.Demo;

public static class Meshes
{
    public static readonly GeometryAsset BigRectangle
        = new AssetFile<GeometryAsset>("./Assets/Meshes/LargeRect.geo").Body;

    public static readonly GeometryAsset SmallRectangle
        = new AssetFile<GeometryAsset>("./Assets/Meshes/SmallRect.geo").Body;
}
