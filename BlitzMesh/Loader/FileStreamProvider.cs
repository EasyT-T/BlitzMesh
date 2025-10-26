namespace BlitzMesh.Loader;

using Stream = System.IO.Stream;

public class FileStreamProvider : IStreamProvider
{
    private string? streamFilename;

    public void SetFromFile(string filename)
    {
        this.streamFilename = filename;
    }

    public Stream GetStream()
    {
        if (this.streamFilename == null)
        {
            throw new InvalidOperationException("Filename not set");
        }

        return File.OpenRead(this.streamFilename);
    }
}