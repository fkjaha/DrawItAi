using UnityEngine;

public class RotationFollower : TransformFollower
{
    private protected override void SetAutoOffset()
    {
        followOffset = followingTransform.eulerAngles - followedTransform.eulerAngles;
    }

    private protected override void FollowTarget()
    {
        Vector3 followedEulerAngles = followedTransform.eulerAngles;
        followingTransform.eulerAngles = followOffset + followedEulerAngles;
    }
}