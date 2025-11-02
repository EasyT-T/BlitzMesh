namespace BlitzMesh.Node;

using System.Collections.Immutable;

public class WorldObject
    : IWorldObject
{
    internal WorldObject(
        string name,
        ITransform transform,
        IAnimation animation,
        IAnimator animator,
        IMesh mesh,
        ImmutableArray<IWorldObject> children)
    {
        this.Name = name;
        this.Transform = transform;
        this.Animation = animation;
        this.Animator = animator;
        this.Mesh = mesh;
        this.Children = children;
    }

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
        ImmutableArray<IWorldObject> children)
    {
        if (name == this.Name &&
            transform == this.Transform &&
            animation == this.Animation &&
            animator == this.Animator &&
            children == this.Children)
        {
            return this;
        }

        return new WorldObject(name, transform, animation, animator, mesh, children);
    }

    public IWorldObject WithName(string name)
    {
        return this.Update(name, this.Transform, this.Animation, this.Animator, this.Mesh, this.Children);
    }

    public IWorldObject WithTransform(ITransform transform)
    {
        return this.Update(this.Name, transform, this.Animation, this.Animator, this.Mesh, this.Children);
    }

    public IWorldObject WithAnimation(IAnimation animation)
    {
        return this.Update(this.Name, this.Transform, animation, this.Animator, this.Mesh, this.Children);
    }

    public IWorldObject WithMesh(IMesh mesh)
    {
        return this.Update(this.Name, this.Transform, this.Animation, this.Animator, mesh, this.Children);
    }

    public IWorldObject WithChildren(ImmutableArray<IWorldObject> children)
    {
        return this.Update(this.Name, this.Transform, this.Animation, this.Animator, this.Mesh, children);
    }

    public IWorldObject WithAnimator(IAnimator animator)
    {
        return this.Update(this.Name, this.Transform, this.Animation, animator, this.Mesh, this.Children);
    }
}