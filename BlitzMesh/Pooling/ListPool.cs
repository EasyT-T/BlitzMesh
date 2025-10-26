namespace BlitzMesh.Pooling;

public class ListPool<T> : ObjectPool<List<T>>
{
    public static ListPool<T> Shared { get; } = new ListPool<T>();

    protected override void CleanObject(List<T> obj)
    {
        obj.Clear();
    }

    protected override List<T> ConstructObject()
    {
        return [];
    }
}