namespace BlitzMesh.Steps;

using BlitzMesh.Enum;
using BlitzMesh.Extension;
using BlitzMesh.Factory;
using BlitzMesh.Loader;
using BlitzMesh.Node;

public class BrushesParsingStep(IBasicReader reader, IMeshContext context) : IBrushesParsingStep
{
    public void Parse()
    {
        var numTextures = reader.ReadInt();

        while (context.GetChunkSize(reader.Position) > 0)
        {
            var name = reader.ReadString();
            var color = reader.ReadColor();
            var shininess = reader.ReadFloat();
            var blend = (BlendFlags)reader.ReadInt();
            var fx = (FxFlags)reader.ReadInt();
            var textureIds = reader.ReadIntArray(numTextures);
            var textures = new ITexture[numTextures];

            for (var i = 0; i < numTextures; i++)
            {
                var id = textureIds[i];

                if (id < 0)
                {
                    continue;
                }

                textures[i] = context.GetTexture(id);
            }

            var brush = MeshFactory.Brush(name, color, shininess, blend, fx, [..textures]);

            context.AddBrush(brush);
        }
    }

    public async Task ParseAsync(CancellationToken cancellationToken = default)
    {
        var numTextures = await reader.ReadIntAsync(cancellationToken);

        while (context.GetChunkSize(reader.Position) > 0)
        {
            var name = await reader.ReadStringAsync(cancellationToken);
            var color = await reader.ReadColorAsync(cancellationToken);
            var shininess = await reader.ReadFloatAsync(cancellationToken);
            var blend = (BlendFlags)await reader.ReadIntAsync(cancellationToken);
            var fx = (FxFlags)await reader.ReadIntAsync(cancellationToken);
            var textureIds = await reader.ReadIntArrayAsync(numTextures, cancellationToken);
            var textures = new ITexture[numTextures];

            for (var i = 0; i < numTextures; i++)
            {
                var id = textureIds[i];

                if (id < 0)
                {
                    continue;
                }

                textures[i] = context.GetTexture(id);
            }

            var brush = MeshFactory.Brush(name, color, shininess, blend, fx, [..textures]);

            context.AddBrush(brush);
        }
    }
}