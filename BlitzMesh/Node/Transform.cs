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

    public ITransform Update(Vector3 position, Quaternion rotation, Vector3 scale, ITransform? parent)
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

    public ITransform WithPosition(Vector3 position)
    {
        return this.Update(position, this.LocalRotation, this.LocalScale, this.Parent);
    }

    public ITransform WithRotation(Quaternion rotation)
    {
        return this.Update(this.LocalPosition, rotation, this.LocalScale, this.Parent);
    }

    public ITransform WithScale(Vector3 scale)
    {
        return this.Update(this.LocalPosition, this.LocalRotation, scale, this.Parent);
    }

    public ITransform WithParent(ITransform? parent)
    {
        return this.Update(this.LocalPosition, this.LocalRotation, this.LocalScale, parent);
    }
}