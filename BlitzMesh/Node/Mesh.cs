namespace BlitzMesh.Node;

using System.Collections.Immutable;

public class Mesh : IMesh
{
    internal Mesh(
        ImmutableArray<ITriangle> triangles,
        ImmutableArray<IBone> bones,
        IBrush brush)
    {
        this.Triangles = triangles;
        this.Bones = bones;
        this.Brush = brush;
    }

    public ImmutableArray<ITriangle> Triangles { get; }

    public ImmutableArray<IBone> Bones { get; }

    public IBrush Brush { get; }

    public IMesh Update(ImmutableArray<ITriangle> triangles, ImmutableArray<IBone> bones, IBrush brush)
    {
        if (triangles == this.Triangles && bones == this.Bones && brush == this.Brush)
        {
            return this;
        }

        return new Mesh(triangles, bones, brush);
    }

    public IMesh WithTriangles(ImmutableArray<ITriangle> triangles)
    {
        return this.Update(triangles, this.Bones, this.Brush);
    }

    public IMesh WithBrush(IBrush brush)
    {
        return this.Update(this.Triangles, this.Bones, brush);
    }

    public IMesh WithBones(ImmutableArray<IBone> bones)
    {
        return this.Update(this.Triangles, bones, this.Brush);
    }
}