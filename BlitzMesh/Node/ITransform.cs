namespace BlitzMesh.Node;

using System.Numerics;

public interface ITransform
{
    ITransform SetParent(ITransform parent);

    ITransform SetLocalPosition(Vector3 position);

    ITransform SetLocalScale(Vector3 scale);

    ITransform SetLocalRotation(Quaternion rotation);
}