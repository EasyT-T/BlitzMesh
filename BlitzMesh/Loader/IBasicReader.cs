namespace BlitzMesh.Loader;

public interface IBasicReader : IDisposable, IAsyncDisposable
{
    int Position { get; }

    int Length { get; }

    int Seek(int offset, SeekOrigin origin);

    byte ReadByte();

    Task<byte> ReadByteAsync(CancellationToken cancellationToken = default);

    byte[] ReadByteArray(int length);

    Task<byte[]> ReadByteArrayAsync(int length, CancellationToken cancellationToken = default);

    string ReadString();

    Task<string> ReadStringAsync(CancellationToken cancellationToken = default);

    int ReadInt();

    Task<int> ReadIntAsync(CancellationToken cancellationToken = default);

    int[] ReadIntArray(int length);

    Task<int[]> ReadIntArrayAsync(int length, CancellationToken cancellationToken = default);

    float ReadFloat();

    Task<float> ReadFloatAsync(CancellationToken cancellationToken = default);

    float[] ReadFloatArray(int length);

    Task<float[]> ReadFloatArrayAsync(int length, CancellationToken cancellationToken = default);
}