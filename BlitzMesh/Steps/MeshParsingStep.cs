namespace BlitzMesh.Steps;

using BlitzMesh.Factory;
using BlitzMesh.Loader;
using BlitzMesh.Node;

public class MeshParsingStep(
    IBasicReader reader,
    IEnterChunkStep enterChunkStep,
    IExitChunkStep exitChunkStep,
    IVerticesParsingStep verticesParsingStep,
    ITriangleParsingStep triangleParsingStep,
    IMeshContext context)
    : IMeshParsingStep
{
    public IWorldObject Parse()
    {
        var brushId = reader.ReadInt();
        var brush = brushId >= 0 ? context.GetBrush(brushId) : MeshFactory.DefaultBrush;

        context.BeginMesh();

        while (context.GetChunkSize(reader.Position) > 0)
        {
            enterChunkStep.Parse();

            switch (context.GetChunkName())
            {
                case "VRTS":
                    verticesParsingStep.Parse();

                    break;
                case "TRIS":
                    triangleParsingStep.Parse();

                    break;
            }

            exitChunkStep.Parse();
        }

        var mesh = context.EndMesh(brush);

        return mesh;
    }

    public async Task<IWorldObject> ParseAsync(CancellationToken cancellationToken = default)
    {
        var brushId = await reader.ReadIntAsync(cancellationToken);
        var brush = brushId >= 0 ? context.GetBrush(brushId) : MeshFactory.DefaultBrush;

        context.BeginMesh();

        while (context.GetChunkSize(reader.Position) > 0)
        {
            await enterChunkStep.ParseAsync(cancellationToken);

            switch (context.GetChunkName())
            {
                case "VRTS":
                    await verticesParsingStep.ParseAsync(cancellationToken);

                    break;
                case "TRIS":
                    await triangleParsingStep.ParseAsync(cancellationToken);

                    break;
            }

            await exitChunkStep.ParseAsync(cancellationToken);
        }

        var mesh = context.EndMesh(brush);

        return mesh;
    }
}