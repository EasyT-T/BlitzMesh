namespace BlitzMesh.Node;

using System.Collections.Immutable;

public class Mesh : WorldObject
{
    internal Mesh(
        string name,
        ITransform transform,
        IAnimation animation,
        IAnimator animator,
        ImmutableArray<IWorldObject> children,
        ImmutableArray<ITriangle> triangles,
        ImmutableArray<IBone> bones,
        IBrush brush)
        : base(name, transform, animation, animator, children)
    {
        this.Triangles = triangles;
        this.Bones = bones;
        this.Brush = brush;
    }

    public ImmutableArray<ITriangle> Triangles { get; }

    public ImmutableArray<IBone> Bones { get; }

    public IBrush Brush { get; }

    public Mesh Update(ImmutableArray<ITriangle> triangles, ImmutableArray<IBone> bones, IBrush brush)
    {
        if (triangles == this.Triangles && bones == this.Bones && brush == this.Brush)
        {
            return this;
        }

        return new Mesh(this.Name, this.Transform, this.Animation, this.Animator, this.Children, triangles, bones, brush);
    }

    public Mesh WithTriangles(ImmutableArray<ITriangle> triangles)
    {
        return this.Update(triangles, this.Bones, this.Brush);
    }

    public Mesh WithBrush(IBrush brush)
    {
        return this.Update(this.Triangles, this.Bones, brush);
    }

    public Mesh WithBones(ImmutableArray<IBone> bones)
    {
        return this.Update(this.Triangles, bones, this.Brush);
    }
}