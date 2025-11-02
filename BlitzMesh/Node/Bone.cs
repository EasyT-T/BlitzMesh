namespace BlitzMesh.Node;

public class Bone : IBone
{
    internal Bone(IVertex vertex, float weight)
    {
        this.Vertex = vertex;
        this.Weight = weight;
    }

    public IVertex Vertex { get; }

    public float Weight { get; }

    public IBone Update(IVertex vertex, float weight)
    {
        if (vertex == this.Vertex && Math.Abs(weight - this.Weight) < float.Epsilon)
        {
            return this;
        }

        return new Bone(vertex, weight);
    }

    public IBone WithVertex(IVertex vertex)
    {
        return this.Update(vertex, this.Weight);
    }

    public IBone WithWeight(float weight)
    {
        return this.Update(this.Vertex, weight);
    }
}