using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class FastBind
{
    public KeyCode GetKeyCode => keyCode;
    public bool IsUsingCombination => isUsingCombination;
    public KeyCode[] GetCombinationKeyCodes => keyCodes;
    public bool IsAnyKey => anyKey;


    [SerializeField] private KeyCode keyCode;
    [SerializeField] private UnityEvent unityEvent;
    [Space(25f)] 
    [SerializeField] private bool isUsingCombination;
    [SerializeField] private KeyCode[] keyCodes;
    [Space(25f)] 
    [SerializeField] private bool anyKey;

    public void ExecuteEvent()
    {
        unityEvent.Invoke();
    }
}