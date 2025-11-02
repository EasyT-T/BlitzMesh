namespace BlitzMesh.Node;

using System.Collections.Immutable;

public interface IWorldObject
{
    public string Name { get; }

    public ITransform Transform { get; }

    public IAnimation Animation { get; }

    public IAnimator Animator { get; }

    public IMesh Mesh { get; }

    public ImmutableArray<IWorldObject> Children { get; }

    public IWorldObject Update(
        string name,
        ITransform transform,
        IAnimation animation,
        IAnimator animator,
        IMesh mesh,
        ImmutableArray<IWorldObject> children);

    public IWorldObject WithName(string name);

    public IWorldObject WithTransform(ITransform transform);

    public IWorldObject WithAnimation(IAnimation animation);

    public IWorldObject WithAnimator(IAnimator animator);

    public IWorldObject WithMesh(IMesh mesh);

    public IWorldObject WithChildren(ImmutableArray<IWorldObject> children);
}