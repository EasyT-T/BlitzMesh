namespace BlitzMesh.Node;

using System.Collections.Immutable;
using System.Numerics;

public class Animation : IAnimation
{
    internal Animation(
        IImmutableDictionary<int, Vector3> positions,
        IImmutableDictionary<int, Vector3> scale,
        IImmutableDictionary<int, Quaternion> rotations)
    {
        this.Positions = positions.ToImmutableSortedDictionary();
        this.Scale = scale.ToImmutableSortedDictionary();
        this.Rotations = rotations.ToImmutableSortedDictionary();
    }

    public ImmutableSortedDictionary<int, Vector3> Positions { get; }

    public ImmutableSortedDictionary<int, Vector3> Scale { get; }

    public ImmutableSortedDictionary<int, Quaternion> Rotations { get; }

    public Animation Update(
        IImmutableDictionary<int, Vector3> positions,
        IImmutableDictionary<int, Vector3> scale,
        IImmutableDictionary<int, Quaternion> rotations)
    {
        if (Equals(positions, this.Positions) && Equals(scale, this.Scale) && Equals(rotations, this.Rotations))
        {
            return this;
        }

        return new Animation(positions, scale, rotations);
    }

    public Animation WithPositions(ImmutableSortedDictionary<int, Vector3> positions)
    {
        return this.Update(positions, this.Scale, this.Rotations);
    }

    public Animation WithScale(ImmutableSortedDictionary<int, Vector3> scale)
    {
        return this.Update(this.Positions, scale, this.Rotations);
    }

    public Animation WithRotations(ImmutableSortedDictionary<int, Quaternion> rotations)
    {
        return this.Update(this.Positions, this.Scale, rotations);
    }
}