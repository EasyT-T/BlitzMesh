namespace BlitzMesh.Node;

using System.Collections.Immutable;
using System.Numerics;

public interface IAnimation
{
    ImmutableSortedDictionary<int, Vector3> Positions { get; }

    ImmutableSortedDictionary<int, Vector3> Scale { get; }

    ImmutableSortedDictionary<int, Quaternion> Rotations { get; }

    IAnimation Update(
        IImmutableDictionary<int, Vector3> positions,
        IImmutableDictionary<int, Vector3> scale,
        IImmutableDictionary<int, Quaternion> rotations);

    IAnimation WithPositions(ImmutableSortedDictionary<int, Vector3> positions);

    IAnimation WithScale(ImmutableSortedDictionary<int, Vector3> scale);

    IAnimation WithRotations(ImmutableSortedDictionary<int, Quaternion> rotations);
}