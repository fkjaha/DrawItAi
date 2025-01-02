using System.Collections.Generic;
using UnityEngine;

public class AToggleableDisabled : AToggleable
{
    [SerializeField] private bool enableOnStart;

    private readonly List<MonoBehaviour> _enableClients = new();
    private int _enableClientsCount;

    private protected override void Start()
    {
        base.Start();
        if(enableOnStart) Enable();
    }

    public override void Toggle(MonoBehaviour client)
    {
        if (_enableClients.Contains(client))
        {
            Disable(client);
        }
        else
        {
            Enable(client);
        }
    }

    public override void Enable(MonoBehaviour client)
    {
        if(_enableClients.Contains(client)) return;
        
        _enableClients.Add(client);
        _enableClientsCount++;
        UpdateState();
    }
    
    public override void Disable(MonoBehaviour client)
    {
        if(!_enableClients.Contains(client)) return;

        _enableClientsCount--;
        _enableClients.Remove(client);
        UpdateState();
    }

    private protected override bool DetermineEnabledState()
    {
        return _enableClientsCount > 0;
    }
    
    public override void DiscardClientRequests(MonoBehaviour client)
    {
        Disable(client);
    }
}