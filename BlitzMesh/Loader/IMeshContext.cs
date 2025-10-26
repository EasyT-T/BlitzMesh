namespace BlitzMesh.Loader;

using BlitzMesh.Node;

public interface IMeshContext
{
    void AddTexture(ITexture texture);

    ITexture GetTexture(int slot);

    void AddBrush(IBrush brush);

    IBrush GetBrush(int slot);

    void AddVertex(IVertex vertex);

    IVertex GetVertex(int slot);

    void AddTriangle(ITriangle triangle);

    ITriangle GetTriangle(int slot);

    void AddBone(IBone bone);

    void BeginMesh();

    IWorldObject EndMesh(IBrush brush);

    void EnterChunk(string name, int endPosition);

    int ExitChunk();

    int GetChunkSize(int position);

    string GetChunkName();
}