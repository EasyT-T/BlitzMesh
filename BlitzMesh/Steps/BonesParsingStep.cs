namespace BlitzMesh.Steps;

using BlitzMesh.Factory;
using BlitzMesh.Loader;
using BlitzMesh.Node;

public class BonesParsingStep(IBasicReader reader, IMeshContext context) : IBonesParsingStep
{
    public IWorldObject Parse()
    {
        while (context.GetChunkSize(reader.Position) > 0)
        {
            var vertexId = reader.ReadInt();
            var vertex = context.GetVertex(vertexId);

            var weight = reader.ReadFloat();

            var bone = MeshFactory.Bone(vertex, weight);

            context.AddBone(bone);
        }

        return MeshFactory.DefaultObject;
    }

    public async Task<IWorldObject> ParseAsync(CancellationToken cancellationToken = default)
    {
        while (context.GetChunkSize(reader.Position) > 0)
        {
            var vertexId = await reader.ReadIntAsync(cancellationToken);
            var vertex = context.GetVertex(vertexId);

            var weight = await reader.ReadFloatAsync(cancellationToken);

            var bone = MeshFactory.Bone(vertex, weight);

            context.AddBone(bone);
        }

        return MeshFactory.DefaultObject;
    }
}