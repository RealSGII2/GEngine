using GEngine.Engine.Graphics;

namespace GEngine.Demo.Old;

public static class Meshes
{
    public static readonly Geometry BigRectangle = new()
    {
        Mesh = new()
        {
            Vertices = new float[] {
                0.5f,  0.5f, 0.0f,
                0.5f, -0.5f, 0.0f,
                -0.5f, -0.5f, 0.0f,
                -0.5f,  0.5f, 0.0f
            },

            Indices = new uint[] {
                0u, 1u, 3u,
                1u, 2u, 3u
            }
        },

        VertexScript = @"
        #version 330 core

        layout (location = 0) in vec3 aPosition;

        void main()
        {
            gl_Position = vec4(aPosition, 1.0);
        }",
        
        FragScript = @"
        #version 330 core

        out vec4 out_color;

        void main()
        {
            out_color = vec4(1.0, 0.5, 0.2, 1.0);
        }"
    };

    public static readonly Geometry SmallRectangle = new()
    {
        Mesh = new()
        {
            Vertices = new float[] {
                0.2f,  0.2f, 0.0f,
                0.2f, -0.2f, 0.0f,
                -0.2f, -0.2f, 0.0f,
                -0.2f,  0.2f, 0.0f
            },

            Indices = new uint[] {
                0u, 1u, 3u,
                1u, 2u, 3u
            }
        },

        VertexScript = @"
        #version 330 core

        layout (location = 0) in vec3 aPosition;

        void main()
        {
            gl_Position = vec4(aPosition, 1.0);
        }",
        
        FragScript = @"
        #version 330 core

        out vec4 out_color;

        void main()
        {
            out_color = vec4(0.0, 0.5, 0.2, 1.0);
        }"
    };
}
