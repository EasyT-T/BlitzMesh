namespace BlitzMesh.Node;

using System.Numerics;

public class Transform : ITransform
{
    internal Transform(Vector3 position, Quaternion rotation, Vector3 scale, ITransform? parent)
    {
        this.LocalPosition = position;
        this.LocalRotation = rotation;
        this.LocalScale = scale;
        this.Parent = parent;
    }

    public Vector3 LocalPosition { get; }

    public Quaternion LocalRotation { get; }

    public Vector3 LocalScale { get; }

    public ITransform? Parent { get; }

    public Transform Update(Vector3 position, Quaternion rotation, Vector3 scale, ITransform? parent)
    {
        if (position == this.LocalPosition &&
            rotation == this.LocalRotation &&
            scale == this.LocalScale &&
            parent == this.Parent)
        {
            return this;
        }

        return new Transform(position, rotation, scale, parent);
    }

    public Transform WithPosition(Vector3 position)
    {
        return this.Update(position, this.LocalRotation, this.LocalScale, this.Parent);
    }

    public Transform WithRotation(Quaternion rotation)
    {
        return this.Update(this.LocalPosition, rotation, this.LocalScale, this.Parent);
    }

    public Transform WithScale(Vector3 scale)
    {
        return this.Update(this.LocalPosition, this.LocalRotation, scale, this.Parent);
    }

    public Transform WithParent(ITransform? parent)
    {
        return this.Update(this.LocalPosition, this.LocalRotation, this.LocalScale, parent);
    }

    ITransform ITransform.SetParent(ITransform parent)
    {
        return this.WithParent(parent);
    }

    ITransform ITransform.SetLocalPosition(Vector3 position)
    {
        return this.WithPosition(position);
    }

    ITransform ITransform.SetLocalScale(Vector3 scale)
    {
        return this.WithScale(scale);
    }

    ITransform ITransform.SetLocalRotation(Quaternion rotation)
    {
        return this.WithRotation(rotation);
    }
}