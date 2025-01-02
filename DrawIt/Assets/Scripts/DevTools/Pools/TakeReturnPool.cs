using System.Collections.Generic;
using UnityEngine;

public abstract class TakeReturnPool<T> : PoolBase<T> where T: UnityEngine.Object
{
    private Stack<T> _freePoolObjects;

    protected override void OnInitializeCollections()
    {
        _freePoolObjects = new ();
    }

    protected override void OnObjectAddedToPool(T poolObject)
    {
        _freePoolObjects.Push(poolObject);
    }

    public bool TakeObject(out T takenObject)
    {
        bool freeObjectExists = _freePoolObjects.TryPop(out takenObject);
        
        if(!freeObjectExists) Debug.LogWarning("No free objects available!", this);
        
        return freeObjectExists;
    }

    public void ReturnObject(T returnedObject)
    {
        if(!PoolObjects.Contains(returnedObject)) return;
        
        _freePoolObjects.Push(returnedObject);
    }
}