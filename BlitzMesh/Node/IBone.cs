namespace BlitzMesh.Node;

public interface IBone
{
    IVertex Vertex { get; }

    float Weight { get; }

    IBone Update(IVertex vertex, float weight);

    IBone WithVertex(IVertex vertex);

    IBone WithWeight(float weight);
}