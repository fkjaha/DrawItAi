using UnityEngine;

public abstract class TransformFollower : AToggleableEnabled
{
    [SerializeField] private protected Transform followingTransform;
    [SerializeField] private protected Transform followedTransform;
    [SerializeField] private bool autoOffsetOnStart;
    [SerializeField] private protected Vector3 followOffset;
    [Space(15f)]
    [SerializeField] private bool isFollowing;

    private protected override void Start()
    {
        base.Start();
        
        if (followedTransform == null || followingTransform == null)
        {
            isFollowing = false;
            Debug.LogWarning("Object follower script has unassigned fields!");
            return;
        }
        if(autoOffsetOnStart) 
            SetAutoOffset();
    }

    private protected abstract void SetAutoOffset();

    private void Update()
    {
        if(!IsEnabled) return;
        
        if (isFollowing)
        {
            FollowTarget();
        }
    }

    private protected abstract void FollowTarget();
}