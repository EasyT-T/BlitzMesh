namespace BlitzMesh.Node;

public interface ITriangle
{
    IVertex VertexA { get; }

    IVertex VertexB { get; }

    IVertex VertexC { get; }

    IBrush Brush { get; }

    ITriangle Update(IVertex v1, IVertex v2, IVertex v3, IBrush brush);

    ITriangle WithVertices(IVertex v1, IVertex v2, IVertex v3);

    ITriangle WithBrush(IBrush brush);
}