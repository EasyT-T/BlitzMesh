namespace BlitzMesh.Node;

public class Triangle : ITriangle
{
    internal Triangle(IVertex v1, IVertex v2, IVertex v3, IBrush brush)
    {
        this.VertexA = v1;
        this.VertexB = v2;
        this.VertexC = v3;
        this.Brush = brush;
    }

    public IVertex VertexA { get; }

    public IVertex VertexB { get; }

    public IVertex VertexC { get; }

    public IBrush Brush { get; }

    public Triangle Update(IVertex v1, IVertex v2, IVertex v3, IBrush brush)
    {
        if (v1 == this.VertexA && v2 == this.VertexB && v3 == this.VertexC && brush == this.Brush)
        {
            return this;
        }

        return new Triangle(v1, v2, v3, brush);
    }

    public Triangle WithVertices(IVertex v1, IVertex v2, IVertex v3)
    {
        return this.Update(v1, v2, v3, this.Brush);
    }

    public Triangle WithBrush(IBrush brush)
    {
        return this.Update(this.VertexA, this.VertexB, this.VertexC, brush);
    }
}