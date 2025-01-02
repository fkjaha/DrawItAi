using UnityEngine;
using UnityEngine.Events;

public class TypeTrigger<T> : MonoBehaviour where T : MonoBehaviour
{
    public event UnityAction OnEntered;
    public event UnityAction OnExit;
    
    public T GetInside => _inside;

    [SerializeField] private UnityEvent onEntered;
    [SerializeField] private UnityEvent onExit;

    private T _inside;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out T tObject)) return;
        
        _inside = tObject;
        InvokeOnEntered();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out T tObject)) return;
        if(tObject != _inside) return;
        if(_inside == null) return;

        _inside = null;
        InvokeOnExit();
    }

    private void InvokeOnEntered()
    {
        OnEntered?.Invoke();
        onEntered.Invoke();
    }
    
    private void InvokeOnExit()
    {
        OnExit?.Invoke();
        onExit.Invoke();
    }
}