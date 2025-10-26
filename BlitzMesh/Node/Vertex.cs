namespace BlitzMesh.Node;

using System.Collections.Immutable;
using System.Drawing;
using System.Numerics;

public class Vertex : IVertex
{
    internal Vertex(Vector3 coordinate, Vector3 normal, Color color, ImmutableArray<Vector2> textureCoordinates)
    {
        this.Coordinate = coordinate;
        this.Normal = normal;
        this.Color = color;
        this.TextureCoordinates = textureCoordinates;
    }

    public Vector3 Coordinate { get; }

    public Vector3 Normal { get; }

    public Color Color { get; }

    public ImmutableArray<Vector2> TextureCoordinates { get; }

    public Vertex Update(Vector3 coordinate, Vector3 normal, Color color, ImmutableArray<Vector2> textureCoordinates)
    {
        if (coordinate == this.Coordinate &&
            normal == this.Normal &&
            color == this.Color &&
            textureCoordinates == this.TextureCoordinates)
        {
            return this;
        }

        return new Vertex(coordinate, normal, color, textureCoordinates);
    }

    public Vertex WithCoordinate(Vector3 coordinate)
    {
        return this.Update(coordinate, this.Normal, this.Color, this.TextureCoordinates);
    }

    public Vertex WithNormal(Vector3 normal)
    {
        return this.Update(this.Coordinate, normal, this.Color, this.TextureCoordinates);
    }

    public Vertex WithColor(Color color)
    {
        return this.Update(this.Coordinate, this.Normal, color, this.TextureCoordinates);
    }

    public Vertex WithTextureCoordinates(ImmutableArray<Vector2> textureCoordinates)
    {
        return this.Update(this.Coordinate, this.Normal, this.Color, textureCoordinates);
    }
}