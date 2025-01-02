using System.Collections.Generic;

public abstract class QueuePool<T> : PoolBase<T> where T: UnityEngine.Object
{
    private Queue<T> _pool;

    protected override void OnInitializeCollections()
    {
        _pool = new Queue<T>();
    }

    protected override void OnObjectAddedToPool(T poolObject)
    {
        _pool.Enqueue(poolObject);
    }

    public T GetNextObject()
    {
        T result = _pool.Dequeue();
        _pool.Enqueue(result);
        return result;
    }
}