using UnityEngine;
using UnityEngine.Events;

public abstract class AToggleable : MonoBehaviour
{
    public event UnityAction<bool> OnToggled;
    
    public bool IsEnabled => _enabled;
    public bool IsDisabled => !_enabled;
    
    private bool _enabled;

    private protected virtual void Start()
    {
        ForceUpdateState();
    }

    public abstract void Toggle(MonoBehaviour client);

    [ContextMenu("Toggle")]
    public void Toggle()
    {
        if(IsEnabled) Disable();
        else Enable();
    }

    public void SetEnabled(bool isEnabled)
    {
        SetEnabled( isEnabled, this);
    }

    public void SetEnabled(bool isEnabled, MonoBehaviour client)
    {   
        if(isEnabled)
            Enable(client);
        else 
            Disable(client);
    }

    public void Enable()
    {
        Enable(this);
    }

    public abstract void Enable(MonoBehaviour client);
    
    public void Disable()
    {
        Disable(this);
    }
    
    public abstract void Disable(MonoBehaviour client);

    private protected void UpdateState()
    {
        bool previousState = _enabled;
        _enabled = DetermineEnabledState();

        if (previousState != _enabled)
        {
            OnToggled?.Invoke(_enabled);
            OnStateChanged(_enabled);
        }
    }
    
    private protected void ForceUpdateState()
    {
        _enabled = DetermineEnabledState();

        OnToggled?.Invoke(_enabled);
        OnStateChanged(_enabled);
    }

    private protected virtual void OnStateChanged(bool isEnabledNewState) { }

    private protected abstract bool DetermineEnabledState();
    
    public abstract void DiscardClientRequests(MonoBehaviour client);
}