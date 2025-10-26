namespace BlitzMesh.Pooling;

using System.Text;

public sealed class StringBuilderPool : ObjectPool<StringBuilder>
{
    public static StringBuilderPool Shared { get; } = new StringBuilderPool();

    protected override void CleanObject(StringBuilder obj)
    {
        obj.Clear();
    }

    protected override StringBuilder ConstructObject()
    {
        return new StringBuilder();
    }
}