namespace BlitzMesh.Steps;

using System.Collections.Immutable;
using System.Numerics;
using BlitzMesh.Enum;
using BlitzMesh.Extension;
using BlitzMesh.Factory;
using BlitzMesh.Loader;
using BlitzMesh.Node;

public class AnimationParsingStep(IBasicReader reader, IMeshContext context) : IAnimationsParsingStep
{
    public IAnimation Parse()
    {
        var flags = (AnimationParsingFlags)reader.ReadInt();

        var positions = new Dictionary<int, Vector3>();
        var scales = new Dictionary<int, Vector3>();
        var rotations = new Dictionary<int, Quaternion>();

        while (context.GetChunkSize(reader.Position) > 0)
        {
            var frame = reader.ReadInt();

            if (flags.HasFlag(AnimationParsingFlags.HasPositionKeys))
            {
                var position = reader.ReadVector3();

                positions.Add(frame, position);
            }

            if (flags.HasFlag(AnimationParsingFlags.HasScaleKeys))
            {
                var scale = reader.ReadVector3();

                scales.Add(frame, scale);
            }

            if (flags.HasFlag(AnimationParsingFlags.HasRotationKeys))
            {
                var rotation = reader.ReadQuaternion();

                rotations.Add(frame, rotation);
            }
        }

        var animation = MeshFactory.Animation(
            positions.ToImmutableDictionary(),
            scales.ToImmutableDictionary(),
            rotations.ToImmutableDictionary());

        return animation;
    }

    public async Task<IAnimation> ParseAsync(CancellationToken cancellationToken = default)
    {
        var flags = (AnimationParsingFlags)await reader.ReadIntAsync(cancellationToken);

        var positions = new Dictionary<int, Vector3>();
        var scales = new Dictionary<int, Vector3>();
        var rotations = new Dictionary<int, Quaternion>();

        while (context.GetChunkSize(reader.Position) > 0)
        {
            var frame = await reader.ReadIntAsync(cancellationToken);

            if (flags.HasFlag(AnimationParsingFlags.HasPositionKeys))
            {
                var position = await reader.ReadVector3Async(cancellationToken);

                positions.Add(frame, position);
            }

            if (flags.HasFlag(AnimationParsingFlags.HasScaleKeys))
            {
                var scale = await reader.ReadVector3Async(cancellationToken);

                scales.Add(frame, scale);
            }

            if (flags.HasFlag(AnimationParsingFlags.HasRotationKeys))
            {
                var rotation = await reader.ReadQuaternionAsync(cancellationToken);

                rotations.Add(frame, rotation);
            }
        }

        var animation = MeshFactory.Animation(
            positions.ToImmutableDictionary(),
            scales.ToImmutableDictionary(),
            rotations.ToImmutableDictionary());

        return animation;
    }
}