namespace BlitzMesh.Node;

using System.Numerics;

public interface ITransform
{
    Vector3 LocalPosition { get; }

    Quaternion LocalRotation { get; }

    Vector3 LocalScale { get; }

    ITransform? Parent { get; }

    ITransform Update(Vector3 position, Quaternion rotation, Vector3 scale, ITransform? parent);

    ITransform WithPosition(Vector3 position);

    ITransform WithRotation(Quaternion rotation);

    ITransform WithScale(Vector3 scale);

    ITransform WithParent(ITransform? parent);
}