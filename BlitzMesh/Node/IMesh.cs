namespace BlitzMesh.Node;

using System.Collections.Immutable;

public interface IMesh
{
    ImmutableArray<ITriangle> Triangles { get; }

    ImmutableArray<IBone> Bones { get; }

    IBrush Brush { get; }

    IMesh Update(ImmutableArray<ITriangle> triangles, ImmutableArray<IBone> bones, IBrush brush);

    IMesh WithTriangles(ImmutableArray<ITriangle> triangles);

    IMesh WithBrush(IBrush brush);

    IMesh WithBones(ImmutableArray<IBone> bones);
}