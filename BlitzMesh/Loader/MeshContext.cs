namespace BlitzMesh.Loader;

using BlitzMesh.Factory;
using BlitzMesh.Node;

internal class MeshContext : IMeshContext
{
    private readonly List<IBrush> brushes = [];

    private readonly Stack<Chunk> chunks = new Stack<Chunk>();

    private readonly List<ITexture> textures = [];

    private readonly List<IVertex> vertices = [];

    private readonly List<ITriangle> triangles = [];

    private readonly List<IBone> bones = [];

    private Mesh? mesh;

    public void AddTexture(ITexture texture)
    {
        this.textures.Add(texture);
    }

    public ITexture GetTexture(int slot)
    {
        return this.textures[slot];
    }

    public void AddBrush(IBrush brush)
    {
        this.brushes.Add(brush);
    }

    public IBrush GetBrush(int slot)
    {
        return this.brushes[slot];
    }

    public void AddVertex(IVertex vertex)
    {
        this.vertices.Add(vertex);
    }

    public IVertex GetVertex(int slot)
    {
        return this.vertices[slot];
    }

    public void AddTriangle(ITriangle triangle)
    {
        this.triangles.Add(triangle);
    }

    public ITriangle GetTriangle(int slot)
    {
        return this.triangles[slot];
    }

    public void AddBone(IBone bone)
    {
        this.bones.Add(bone);
    }

    public void BeginMesh()
    {
        this.mesh = MeshFactory.NoneMesh;
    }

    public IMesh EndMesh(IBrush brush)
    {
        var result = this.mesh!
            .WithTriangles(
                [..this.triangles])
            .WithBrush(brush)
            .WithBones([..this.bones]);

        this.bones.Clear();
        this.mesh = null;

        return result;
    }

    public void EnterChunk(string name, int endPosition)
    {
        this.chunks.Push(new Chunk(name, endPosition));
    }

    public int ExitChunk()
    {
        return this.chunks.Pop().EndPosition;
    }

    public int GetChunkSize(int position)
    {
        return this.chunks.Peek().EndPosition - position;
    }

    public string GetChunkName()
    {
        return this.chunks.Peek().Name;
    }

    private readonly struct Chunk(string name, int endPosition)
    {
        public readonly string Name = name;

        public readonly int EndPosition = endPosition;
    }
}