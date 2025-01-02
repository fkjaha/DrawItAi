using UnityEngine;

// [DebugCommandPrefix("cursorVisible")]
public class CursorVisible : AToggleableDisabled
{
    public static CursorVisible Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        transform.parent = null;
        DontDestroyOnLoad(this);
    }
    
    
    // [DebugCommand("set")]
    public void SetVisible(bool isVisible)
    {
        SetEnabled(isVisible);
    }
    
    private protected override void OnStateChanged(bool isEnabledNewState)
    {
        base.OnStateChanged(isEnabledNewState);
        Cursor.visible = isEnabledNewState;
    }
}