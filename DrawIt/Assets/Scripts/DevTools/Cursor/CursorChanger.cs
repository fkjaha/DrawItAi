using System;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] private bool disableSetOnStart;
    [SerializeField] private bool useOnEnableOnDisable;
    [Space(10f)]
    [SerializeField] private bool visibleState;
    [SerializeField] private bool isLockedState;

    private void Start()
    {
        if(disableSetOnStart) return;
        
        SetCursor(visibleState, isLockedState);
    }

    // [DebugCommand]
    private void SetCursor(bool isVisible, bool isLocked)
    {
        SetCursorVisible(isVisible);
        SetIsLocked(isLocked);
    }
    
    // [DebugCommand]
    public void SetCursorVisible(bool isVisible)
    {
        // Cursor.visible = isVisible;
        CursorVisible.Instance.SetEnabled(isVisible, this);
    }
    
    // [DebugCommand]
    public void SetIsLocked(bool isLocked)
    {
        // Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        CursorLocked.Instance.SetEnabled(isLocked, this);
    }

    private void OnEnable()
    {
        if (!useOnEnableOnDisable) return;
        if (CursorVisible.Instance == null || CursorLocked.Instance == null) return;
        
        SetCursor(visibleState, isLockedState);
    }

    private void OnDisable()
    {
        if (!useOnEnableOnDisable) return;
        if (CursorVisible.Instance == null || CursorLocked.Instance == null) return;
        
        CursorVisible.Instance.DiscardClientRequests(this);
        CursorLocked.Instance.DiscardClientRequests(this);
    }

    private void OnDestroy()
    {
        CursorVisible.Instance.DiscardClientRequests(this);
        CursorLocked.Instance.DiscardClientRequests(this);
    }
}