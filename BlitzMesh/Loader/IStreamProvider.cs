namespace BlitzMesh.Loader;

using Stream = System.IO.Stream;

public interface IStreamProvider
{
    void SetFromFile(string filename);

    Stream GetStream();
}