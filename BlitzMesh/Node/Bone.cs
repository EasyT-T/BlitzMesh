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

    public Bone Update(IVertex vertex, float weight)
    {
        if (vertex == this.Vertex && Math.Abs(weight - this.Weight) < float.Epsilon)
        {
            return this;
        }

        return new Bone(vertex, weight);
    }

    public Bone WithVertex(IVertex vertex)
    {
        return this.Update(vertex, this.Weight);
    }

    public Bone WithWeight(float weight)
    {
        return this.Update(this.Vertex, weight);
    }
}