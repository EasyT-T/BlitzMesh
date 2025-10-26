namespace BlitzMesh.Node;

using System.Collections.Immutable;

public class WorldObject : IWorldObject
{
    internal WorldObject(
        string name,
        ITransform transform,
        IAnimation animation,
        IAnimator animator,
        ImmutableArray<IWorldObject> children)
    {
        this.Name = name;
        this.Transform = transform;
        this.Animation = animation;
        this.Animator = animator;
        this.Children = children;
    }

    public string Name { get; }

    public ITransform Transform { get; }

    public IAnimation Animation { get; }

    public IAnimator Animator { get; }

    public ImmutableArray<IWorldObject> Children { get; }

    public WorldObject Update(
        string name,
        ITransform transform,
        IAnimation animation,
        IAnimator animator,
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

        return new WorldObject(name, transform, animation, animator, children);
    }

    public WorldObject WithName(string name)
    {
        return this.Update(name, this.Transform, this.Animation, this.Animator, this.Children);
    }

    public WorldObject WithTransform(ITransform transform)
    {
        return this.Update(this.Name, transform, this.Animation, this.Animator, this.Children);
    }

    public WorldObject WithAnimation(IAnimation animation)
    {
        return this.Update(this.Name, this.Transform, animation, this.Animator, this.Children);
    }

    public WorldObject WithChildren(ImmutableArray<IWorldObject> children)
    {
        return this.Update(this.Name, this.Transform, this.Animation, this.Animator, children);
    }

    public WorldObject WithAnimator(IAnimator animator)
    {
        return this.Update(this.Name, this.Transform, this.Animation, animator, this.Children);
    }

    ITransform IWorldObject.GetTransform()
    {
        return this.Transform;
    }

    IWorldObject IWorldObject.SetTransform(ITransform transform)
    {
        return this.WithTransform(transform);
    }

    IWorldObject IWorldObject.AddChild(IWorldObject child)
    {
        return this.WithChildren(this.Children.Add(child));
    }

    IWorldObject IWorldObject.SetName(string name)
    {
        return this.WithName(name);
    }

    IWorldObject IWorldObject.SetAnimation(IAnimation animation)
    {
        return this.WithAnimation(animation);
    }

    IWorldObject IWorldObject.SetAnimator(IAnimator animator)
    {
        return this.WithAnimator(animator);
    }
}