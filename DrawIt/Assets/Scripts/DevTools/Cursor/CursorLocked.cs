using UnityEngine;

// [DebugCommandPrefix("cursorLocked")]
public class CursorLocked : AToggleableEnabled
{
    public static CursorLocked Instance;

    [SerializeField] private CursorLockMode modeWhenUnLocked;

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
    public void SetLocked(bool isLocked)
    {
        SetEnabled(isLocked);
    }
    
    private protected override void OnStateChanged(bool isEnabledNewState)
    {
        base.OnStateChanged(isEnabledNewState);
        Cursor.lockState = isEnabledNewState ? CursorLockMode.Locked : modeWhenUnLocked;
    }
}