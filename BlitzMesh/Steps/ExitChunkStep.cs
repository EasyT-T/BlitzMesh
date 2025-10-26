namespace BlitzMesh.Steps;

using BlitzMesh.Loader;

public class ExitChunkStep(IBasicReader reader, IMeshContext context) : IExitChunkStep
{
    public void Parse()
    {
        var endPosition = context.ExitChunk();

        reader.Seek(endPosition, SeekOrigin.Begin);
    }

    public Task ParseAsync(CancellationToken cancellationToken = default)
    {
        var endPosition = context.ExitChunk();

        reader.Seek(endPosition, SeekOrigin.Begin);

        return Task.CompletedTask;
    }
}