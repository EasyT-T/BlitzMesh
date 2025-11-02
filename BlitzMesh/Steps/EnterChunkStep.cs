namespace BlitzMesh.Steps;

using System.Text;
using BlitzMesh.Loader;

public class EnterChunkStep(IBasicReader reader, IMeshContext context) : IEnterChunkStep
{
    public void Parse()
    {
        var chunkName = Encoding.ASCII.GetString(reader.ReadByteArray(4));
        var chunkSize = reader.ReadInt();

        context.EnterChunk(chunkName, reader.Position + chunkSize);
    }

    public async Task ParseAsync(CancellationToken cancellationToken = default)
    {
        var chunkName = Encoding.ASCII.GetString(await reader.ReadByteArrayAsync(4, cancellationToken));
        var chunkSize = await reader.ReadIntAsync(cancellationToken);

        context.EnterChunk(chunkName, reader.Position + chunkSize);
    }
}