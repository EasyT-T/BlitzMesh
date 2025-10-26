namespace BlitzMesh.Steps;

using System.Drawing;
using System.Numerics;
using BlitzMesh.Enum;
using BlitzMesh.Extension;
using BlitzMesh.Factory;
using BlitzMesh.Loader;

public class VerticesParsingStep(IBasicReader reader, IMeshContext context) : IVerticesParsingStep
{
    public void Parse()
    {
        var flags = (VertexParsingFlags)reader.ReadInt();
        var numTextureCoordinates = reader.ReadInt();
        var textureCoordinateSize = reader.ReadInt();

        if (textureCoordinateSize < 2)
        {
            throw new InvalidDataException("Texture coordinate size must be at least 2");
        }

        while (context.GetChunkSize(reader.Position) > 0)
        {
            var coordinate = new Vector3(reader.ReadFloatArray(3));

            var normal = flags.HasFlag(VertexParsingFlags.HasNormal)
                ? new Vector3(reader.ReadFloatArray(3))
                : Vector3.Zero;

            var color = flags.HasFlag(VertexParsingFlags.HasColor)
                ? reader.ReadColor()
                : Color.White;

            var textureCoordinates = new Vector2[numTextureCoordinates];

            for (var i = 0; i < numTextureCoordinates; i++)
            {
                var tcArray = reader.ReadFloatArray(textureCoordinateSize);

                textureCoordinates[i] = new Vector2(tcArray);
            }

            var vertex = MeshFactory.Vertex(coordinate, normal, color, [..textureCoordinates]);

            context.AddVertex(vertex);
        }
    }

    public async Task ParseAsync(CancellationToken cancellationToken = default)
    {
        var flags = (VertexParsingFlags)await reader.ReadIntAsync(cancellationToken);
        var numTextureCoordinates = await reader.ReadIntAsync(cancellationToken);
        var textureCoordinateSize = await reader.ReadIntAsync(cancellationToken);

        if (textureCoordinateSize < 2)
        {
            throw new InvalidDataException("Texture coordinate size must be at least 2");
        }

        while (context.GetChunkSize(reader.Position) > 0)
        {
            var coordinate = new Vector3(await reader.ReadFloatArrayAsync(3, cancellationToken));

            var normal = flags.HasFlag(VertexParsingFlags.HasNormal)
                ? new Vector3(await reader.ReadFloatArrayAsync(3, cancellationToken))
                : Vector3.Zero;

            var color = flags.HasFlag(VertexParsingFlags.HasColor)
                ? await reader.ReadColorAsync(cancellationToken)
                : Color.White;

            var textureCoordinates = new Vector2[numTextureCoordinates];

            for (var i = 0; i < numTextureCoordinates; i++)
            {
                var tcArray = await reader.ReadFloatArrayAsync(textureCoordinateSize, cancellationToken);

                textureCoordinates[i] = new Vector2(tcArray);
            }

            var vertex = MeshFactory.Vertex(coordinate, normal, color, [..textureCoordinates]);

            context.AddVertex(vertex);
        }
    }
}