namespace BlitzMesh.Pooling;

public class RentObject<TRent> : IDisposable
    where TRent : class
{
    private readonly ObjectPool<TRent> pool;

    internal RentObject(TRent obj, ObjectPool<TRent> pool)
    {
        this.Object = obj;
        this.pool = pool;
    }

    public TRent Object { get; }

    public void Dispose()
    {
        this.pool.Return(this);

        GC.SuppressFinalize(this);
    }
}