using System.Collections.Generic;
using UnityEngine;

public class AToggleableEnabled : AToggleable
{
    [SerializeField] private bool disableOnStart;
    
    private readonly List<MonoBehaviour> _disableClients = new();
    private int _disableClientsCount;

    private protected override void Start()
    {
        base.Start();
        if(disableOnStart) Disable();
    }
    
    public override void Toggle(MonoBehaviour client)
    {
        if (_disableClients.Contains(client))
        {
            Enable(client);
        }
        else
        {
            Disable(client);
        }
    }

    public override void Disable(MonoBehaviour client)
    {
        if(_disableClients.Contains(client)) return;
        
        _disableClients.Add(client);
        _disableClientsCount++;
        UpdateState();
    }
    
    public override void Enable(MonoBehaviour client)
    {
        if(!_disableClients.Contains(client)) return;

        _disableClientsCount--;
        _disableClients.Remove(client);
        UpdateState();
    }

    private protected override bool DetermineEnabledState()
    {
        return _disableClientsCount < 1;
    }

    public override void DiscardClientRequests(MonoBehaviour client)
    {
        Enable(client);
    }
}
