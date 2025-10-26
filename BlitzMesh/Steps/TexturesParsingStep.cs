namespace BlitzMesh.Steps;

using System.Numerics;
using BlitzMesh.Enum;
using BlitzMesh.Factory;
using BlitzMesh.Loader;

public class TexturesParsingStep(IBasicReader reader, IMeshContext context) : ITexturesParsingStep
{
    public void Parse()
    {
        while (context.GetChunkSize(reader.Position) > 0)
        {
            var name = reader.ReadString();
            var flags = (TextureFlag)reader.ReadInt();
            var blend = (BlendFlags)reader.ReadInt();
            var position = new Vector2(reader.ReadFloatArray(2));
            var scale = new Vector2(reader.ReadFloatArray(2));
            var rotation = reader.ReadFloat();

            var texture = MeshFactory.Texture(name, flags, position, rotation, scale, blend);

            context.AddTexture(texture);
        }
    }

    public async Task ParseAsync(CancellationToken cancellationToken = default)
    {
        while (context.GetChunkSize(reader.Position) > 0)
        {
            var name = await reader.ReadStringAsync(cancellationToken);
            var flags = (TextureFlag)await reader.ReadIntAsync(cancellationToken);
            var blend = (BlendFlags)await reader.ReadIntAsync(cancellationToken);
            var position = new Vector2(await reader.ReadFloatArrayAsync(2, cancellationToken));
            var scale = new Vector2(await reader.ReadFloatArrayAsync(2, cancellationToken));
            var rotation = await reader.ReadFloatAsync(cancellationToken);

            var texture = MeshFactory.Texture(name, flags, position, rotation, scale, blend);

            context.AddTexture(texture);
        }
    }
}