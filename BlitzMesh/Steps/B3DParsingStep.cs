namespace BlitzMesh.Steps;

using BlitzMesh.Loader;
using BlitzMesh.Node;

public class B3DParsingStep(
    IBasicReader reader,
    IMeshContext context,
    IEnterChunkStep enterChunkStep,
    IExitChunkStep exitChunkStep,
    ITexturesParsingStep texturesParsingStep,
    IBrushesParsingStep brushesParsingStep,
    INodeParsingStep nodeParsingStep) : IB3DParsingStep
{
    public IWorldObject? Parse()
    {
        enterChunkStep.Parse();

        if (context.GetChunkName() != "BB3D")
        {
            throw new InvalidDataException("File is not a valid B3D file");
        }

        var version = reader.ReadInt();

        if (version > 1)
        {
            throw new PlatformNotSupportedException("B3D file's struct version not supported");
        }

        IWorldObject? obj = null;

        while (context.GetChunkSize(reader.Position) > 0)
        {
            enterChunkStep.Parse();

            switch (context.GetChunkName())
            {
                case "TEXS":
                    texturesParsingStep.Parse();

                    break;
                case "BRUS":
                    brushesParsingStep.Parse();

                    break;
                case "NODE":
                    obj = nodeParsingStep.Parse();

                    break;
            }

            exitChunkStep.Parse();
        }

        return obj;
    }

    public async Task<IWorldObject?> ParseAsync(CancellationToken cancellationToken = default)
    {
        await enterChunkStep.ParseAsync(cancellationToken);

        if (context.GetChunkName() != "BB3D")
        {
            throw new InvalidDataException("File is not a valid B3D file");
        }

        var version = await reader.ReadIntAsync(cancellationToken);

        if (version > 1)
        {
            throw new PlatformNotSupportedException("B3D file's struct version not supported");
        }

        IWorldObject? obj = null;

        while (context.GetChunkSize(reader.Position) > 0)
        {
            await enterChunkStep.ParseAsync(cancellationToken);

            switch (context.GetChunkName())
            {
                case "TEXS":
                    await texturesParsingStep.ParseAsync(cancellationToken);

                    break;
                case "BRUS":
                    await brushesParsingStep.ParseAsync(cancellationToken);

                    break;
                case "NODE":
                    obj = await nodeParsingStep.ParseAsync(cancellationToken);

                    break;
            }

            await exitChunkStep.ParseAsync(cancellationToken);
        }

        return obj;
    }
}