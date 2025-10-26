namespace BlitzMesh.Pooling;

using System.Collections.Concurrent;

public abstract class ObjectPool<T>
    where T : class
{
    private readonly ConcurrentBag<T> bag = [];

    public RentObject<T> Rent()
    {
        return this.bag.TryTake(out var obj)
            ? new RentObject<T>(obj, this)
            : new RentObject<T>(this.ConstructObject(), this);
    }

    public void Return(RentObject<T> obj)
    {
        var obj2 = obj.Object;

        this.CleanObject(obj2);

        this.bag.Add(obj2);
    }

    protected abstract void CleanObject(T obj);

    protected abstract T ConstructObject();
}