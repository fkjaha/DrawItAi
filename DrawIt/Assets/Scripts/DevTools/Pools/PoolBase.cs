using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolBase<T> : MonoBehaviour where T: UnityEngine.Object
{
    public IReadOnlyList<T> GetAllPoolObjects => PoolObjects;
    public Transform GetPoolParent => poolParent;

    [SerializeField] private T instancePrefab;
    [SerializeField] private Transform poolParent;
    [SerializeField] private bool spawnInLocalPosition;
    [SerializeField] private int poolSize;
    [SerializeField] private bool optimizeInitialization;

    protected List<T> PoolObjects;

    protected virtual void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (optimizeInitialization)
        {
            StartCoroutine(OptimizedInitialization());
            return;
        }
        
        DefaultInitialization();
    }
    
    private void DefaultInitialization()
    {
        InitializeCollections();
        for (int i = 0; i < poolSize; i++)
        {
            AddOneObjectToPool();
        }
    }

    private IEnumerator OptimizedInitialization()
    {
        InitializeCollections();
        for (int i = 0; i < poolSize; i++)
        {
            AddOneObjectToPool();
            yield return null;
        }
    }

    private void InitializeCollections()
    {
        PoolObjects = new ();
        OnInitializeCollections();
    }
    
    protected virtual void OnInitializeCollections() { }

    private void AddOneObjectToPool()
    {
        T poolObject = Instantiate(instancePrefab, spawnInLocalPosition ? poolParent.position : Vector3.zero,
            Quaternion.identity, poolParent);
        
        PoolObjects.Add(poolObject);
        OnObjectAddedToPool(poolObject);
    }
    
    protected virtual void OnObjectAddedToPool(T poolObject) { }
}