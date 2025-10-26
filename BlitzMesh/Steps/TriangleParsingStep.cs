namespace BlitzMesh.Steps;

using System.Collections.Immutable;
using BlitzMesh.Factory;
using BlitzMesh.Loader;

public class TriangleParsingStep(IBasicReader reader, IMeshContext context) : ITriangleParsingStep
{
    public void Parse()
    {
        var brushId = reader.ReadInt();
        var brush = brushId >= 0 ? context.GetBrush(brushId) : MeshFactory.DefaultBrush;

        while (context.GetChunkSize(reader.Position) > 0)
        {
            var vertexIds = reader.ReadIntArray(3);
            var vertices = vertexIds.Select(context.GetVertex).ToImmutableArray();

            var triangle = MeshFactory.Triangle(vertices[0], vertices[1], vertices[2], brush);

            context.AddTriangle(triangle);
        }
    }

    public async Task ParseAsync(CancellationToken cancellationToken = default)
    {
        var brushId = await reader.ReadIntAsync(cancellationToken);
        var brush = brushId >= 0 ? context.GetBrush(brushId) : MeshFactory.DefaultBrush;

        while (context.GetChunkSize(reader.Position) > 0)
        {
            var vertexIds = await reader.ReadIntArrayAsync(3, cancellationToken);
            var vertices = vertexIds.Select(context.GetVertex).ToImmutableArray();

            var triangle = MeshFactory.Triangle(vertices[0], vertices[1], vertices[2], brush);

            context.AddTriangle(triangle);
        }
    }
}