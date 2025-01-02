using System;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{
    public event UnityAction OnTriggered;
    
    [SerializeField] private UnityEvent onTriggered;
    [SerializeField] private bool disableOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TriggersTriggers _))
        {
            Trigger();
        }
    }

    private void Trigger()
    {
        OnTriggered?.Invoke();
        onTriggered.Invoke();

        if (disableOnTrigger) gameObject.SetActive(false);
    }
}