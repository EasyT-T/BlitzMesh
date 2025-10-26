namespace BlitzMesh.Extension;

using System.Drawing;
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
}