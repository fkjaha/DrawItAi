using System;
using UnityEngine;

public abstract class ScriptableType : ScriptableObject
{
    public string GetTypeName => typeName;

    [SerializeField] private string typeName;
    [SerializeField] private bool disableAutoName;
    
    protected void OnValidate()
    {
        if (!disableAutoName && typeName == String.Empty) typeName = name;
    }
}
