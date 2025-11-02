namespace BlitzMesh.Extension;

using System.Drawing;
using System.Numerics;
using BlitzMesh.Loader;

public static class BasicReaderExtensions
{
    public static Color ReadColor(this IBasicReader basicReader)
    {
        var rgba = basicReader.ReadFloatArray(4);

        var r = (int)(rgba[0] * 255.0f);
        var g = (int)(rgba[1] * 255.0f);
        var b = (int)(rgba[2] * 255.0f);
        var a = (int)(rgba[3] * 255.0f);

        return Color.FromArgb(a, r, g, b);
    }

    public static async Task<Color> ReadColorAsync(
        this IBasicReader basicReader,
        CancellationToken cancellationToken = default)
    {
        var rgba = await basicReader.ReadFloatArrayAsync(4, cancellationToken);

        var r = (int)(rgba[0] * 255.0f);
        var g = (int)(rgba[1] * 255.0f);
        var b = (int)(rgba[2] * 255.0f);
        var a = (int)(rgba[3] * 255.0f);

        return Color.FromArgb(a, r, g, b);
    }

    public static Vector2 ReadVector2(this IBasicReader basicReader)
    {
        var array = basicReader.ReadFloatArray(2);

        var x = array[0];
        var y = array[1];

        return new Vector2(x, y);
    }

    public static async Task<Vector2> ReadVector2Async(
        this IBasicReader basicReader,
        CancellationToken cancellationToken = default)
    {
        var array = await basicReader.ReadFloatArrayAsync(2, cancellationToken);

        var x = array[0];
        var y = array[1];

        return new Vector2(x, y);
    }

    public static Vector3 ReadVector3(this IBasicReader basicReader)
    {
        var array = basicReader.ReadFloatArray(3);

        var x = array[0];
        var y = array[1];
        var z = array[2];

        return new Vector3(x, y, z);
    }

    public static async Task<Vector3> ReadVector3Async(this IBasicReader basicReader, CancellationToken cancellationToken = default)
    {
        var array = await basicReader.ReadFloatArrayAsync(3, cancellationToken);

        var x = array[0];
        var y = array[1];
        var z = array[2];

        return new Vector3(x, y, z);
    }

    public static Quaternion ReadQuaternion(this IBasicReader basicReader)
    {
        var array = basicReader.ReadFloatArray(4);

        var x = array[0];
        var y = array[1];
        var z = array[2];
        var w = array[3];

        return new Quaternion(x, y, z, w);
    }

    public static async Task<Quaternion> ReadQuaternionAsync(this IBasicReader basicReader, CancellationToken cancellationToken = default)
    {
        var array = await basicReader.ReadFloatArrayAsync(4, cancellationToken);

        var x = array[0];
        var y = array[1];
        var z = array[2];
        var w = array[3];

        return new Quaternion(x, y, z, w);
    }
}