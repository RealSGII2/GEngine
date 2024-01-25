<Root>
    <Head v="0.0.1" />

    <GeometryAsset bind="LargeRect.obj">
        <!-- This will eventually be replaced by a general mesh renderer. -->
        <VertexScript source="/Shaders/Vertex.glsl" />
        <Scale>1</Scale>

        <FragScript source="/Shaders/Orange.glsl">
            <Parameter name="AlbeidoTexture">
                <TextureMap source="/Textures/White.dds" />
            </Parameter>
            <Parameter name="AlbeidoMultiply">
                <Rgb r="1" g="0.5" b="0.2" a="1" />
            </Parameter>
        </FragScript>
    </GeometryAssets>
</Root>
