namespace BlitzMesh.Stream;

using System.Buffers;
using BlitzMesh.Loader;
using BlitzMesh.Pooling;
using Stream = System.IO.Stream;

public sealed class StreamBasicReader(Stream stream, bool leaveOpen = false)
    : IBasicReader
{
    public StreamBasicReader(IStreamProvider streamProvider, bool leaveOpen = false)
        : this(streamProvider.GetStream())
    {
    }

    public int Position => (int)stream.Position;

    public int Length => (int)stream.Length;

    public int Seek(int offset, SeekOrigin origin)
    {
        return (int)stream.Seek(offset, origin);
    }

    public byte ReadByte()
    {
        Span<byte> buffer = stackalloc byte[sizeof(byte)];

        var cnt = stream.Read(buffer);

        return cnt == 0 ? throw new EndOfStreamException("Reached end of stream") : buffer[0];
    }

    public async Task<byte> ReadByteAsync(CancellationToken cancellationToken = default)
    {
        using var memoryOwner = MemoryPool<byte>.Shared.Rent(sizeof(byte));
        var buffer = memoryOwner.Memory[..sizeof(byte)];

        var cnt = await stream.ReadAsync(buffer, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        return cnt == 0 ? throw new EndOfStreamException("Reached end of stream") : buffer.Span[0];
    }

    public byte[] ReadByteArray(int length)
    {
        var array = new byte[length];

        for (var i = 0; i < array.Length; i++)
        {
            array[i] = this.ReadByte();
        }

        return array;
    }

    public async Task<byte[]> ReadByteArrayAsync(int length, CancellationToken cancellationToken = default)
    {
        var array = new byte[length];

        for (var i = 0; i < array.Length; i++)
        {
            array[i] = await this.ReadByteAsync(cancellationToken);
        }

        return array;
    }

    public string ReadString()
    {
        using var stringBuilder = StringBuilderPool.Shared.Rent();

        Span<byte> buffer = stackalloc byte[8];

        while (true)
        {
            var cnt = stream.Read(buffer);

            if (cnt == 0)
            {
                throw new EndOfStreamException("Reached end of stream without null terminated.");
            }

            for (var i = 0; i < cnt; i++)
            {
                var c = (char)buffer[i];
                if (c == '\0')
                {
                    stream.Seek(-(cnt - i - 1), SeekOrigin.Current);

                    return stringBuilder.Object.ToString();
                }

                stringBuilder.Object.Append(c);
            }
        }
    }

    public async Task<string> ReadStringAsync(CancellationToken cancellationToken = default)
    {
        using var stringBuilder = StringBuilderPool.Shared.Rent();

        using var memoryOwner = MemoryPool<byte>.Shared.Rent(8);
        var buffer = memoryOwner.Memory[..8];

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var cnt = await stream.ReadAsync(buffer, cancellationToken);

            var span = buffer.Span;

            if (cnt == 0)
            {
                throw new EndOfStreamException("Reached end of stream without null terminated.");
            }

            for (var i = 0; i < cnt; i++)
            {
                var c = (char)span[i];
                if (c == '\0')
                {
                    stream.Seek(-(cnt - i - 1), SeekOrigin.Current);

                    return stringBuilder.Object.ToString();
                }

                stringBuilder.Object.Append(c);
            }
        }
    }

    public int ReadInt()
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];

        var cnt = stream.Read(buffer);

        return cnt >= sizeof(int)
            ? BitConverter.ToInt32(buffer)
            : throw new EndOfStreamException("Reached end of stream.");
    }

    public async Task<int> ReadIntAsync(CancellationToken cancellationToken = default)
    {
        using var memoryOwner = MemoryPool<byte>.Shared.Rent(sizeof(int));
        var buffer = memoryOwner.Memory[..sizeof(int)];

        var cnt = await stream.ReadAsync(buffer, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        return cnt >= sizeof(int)
            ? BitConverter.ToInt32(buffer.Span)
            : throw new EndOfStreamException("Reached end of stream.");
    }

    public int[] ReadIntArray(int length)
    {
        var array = new int[length];

        for (var i = 0; i < array.Length; i++)
        {
            array[i] = this.ReadInt();
        }

        return array;
    }

    public async Task<int[]> ReadIntArrayAsync(int length, CancellationToken cancellationToken = default)
    {
        var array = new int[length];

        for (var i = 0; i < array.Length; i++)
        {
            array[i] = await this.ReadIntAsync(cancellationToken);
        }

        return array;
    }

    public float ReadFloat()
    {
        Span<byte> buffer = stackalloc byte[sizeof(float)];

        var cnt = stream.Read(buffer);

        return cnt >= sizeof(float)
            ? BitConverter.ToSingle(buffer)
            : throw new EndOfStreamException("Reached end of stream.");
    }

    public async Task<float> ReadFloatAsync(CancellationToken cancellationToken = default)
    {
        using var memoryOwner = MemoryPool<byte>.Shared.Rent(sizeof(float));
        var buffer = memoryOwner.Memory[..sizeof(float)];

        var cnt = await stream.ReadAsync(buffer, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        return cnt >= sizeof(float)
            ? BitConverter.ToSingle(buffer.Span)
            : throw new EndOfStreamException("Reached end of stream.");
    }

    public float[] ReadFloatArray(int length)
    {
        var array = new float[length];

        for (var i = 0; i < array.Length; i++)
        {
            array[i] = this.ReadFloat();
        }

        return array;
    }

    public async Task<float[]> ReadFloatArrayAsync(int length, CancellationToken cancellationToken = default)
    {
        var array = new float[length];

        for (var i = 0; i < array.Length; i++)
        {
            array[i] = await this.ReadFloatAsync(cancellationToken);
        }

        return array;
    }

    public void Dispose()
    {
        this.Dispose(true);
    }

    public async ValueTask DisposeAsync()
    {
        await this.DisposeAsync(true);
    }

    internal void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        if (!leaveOpen)
        {
            stream.Dispose();
        }
    }

    internal async ValueTask DisposeAsync(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        if (!leaveOpen)
        {
            await stream.DisposeAsync();
        }
    }
}