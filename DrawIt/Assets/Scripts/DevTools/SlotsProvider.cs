using System.Collections.Generic;
using UnityEngine;

public abstract class SlotsProvider<T> : MonoBehaviour where T: MonoBehaviour
{
    public List<T> GetSlots => _createdSlots;
    
    [SerializeField] private T slotPrefab;
    [SerializeField] private Transform slotsParent;
    [Header("Optional")]
    [SerializeField] private Transform startingPointChild;
    [SerializeField] private bool spawnAboveStartingPoint;
    
    private List<T> _createdSlots;
    private bool _hintsPageIsUpToDate;

    private void Awake()
    {
        InitializeSlotsListIfNecessary();
    }

    public List<T> RequestSlots(int count)
    {
        InitializeSlotsListIfNecessary();

        if (_createdSlots.Count >= count)
        {
            int overflowNumberOfSlots = _createdSlots.Count - count;
            DestroySlots(overflowNumberOfSlots);
        }
        else
        {
            int lackingNumberOfSlots = count - _createdSlots.Count;
            CreateSlots(lackingNumberOfSlots);
        }
        
        return _createdSlots;
    }

    private void CreateSlots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            T slot = Instantiate(slotPrefab, slotsParent);
            _createdSlots.Add(slot);
            
            if(startingPointChild == null) continue;
            
            int startingChildIndex = startingPointChild.GetSiblingIndex();
            slot.transform.SetSiblingIndex(startingChildIndex + spawnAboveStartingPoint.T0F1());
            // Debug.Log($"{startingChildIndex} | {spawnAboveStartingPoint.TrueToZeroFalseToOne()} | {slot.transform.GetSiblingIndex()}");
        }
    }

    private void DestroySlots(int count)
    {
        List<T> slotsToDestroy = _createdSlots.GetRange(0, count);
        foreach (T hintSlot in slotsToDestroy)
        {
            _createdSlots.Remove(hintSlot);
            Destroy(hintSlot?.gameObject);
        }
    }

    private void InitializeSlotsListIfNecessary()
    {
        if (_createdSlots == null)
        {
            _createdSlots = new();
        }
    }
}
