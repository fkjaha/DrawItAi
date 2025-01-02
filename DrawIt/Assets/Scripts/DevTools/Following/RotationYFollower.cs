using UnityEngine;

public class RotationYFollower : TransformFollower
{
    private protected override void SetAutoOffset()
    {
        followOffset = followingTransform.eulerAngles - followedTransform.eulerAngles;
    }

    private protected override void FollowTarget()
    {
        Vector3 followedEulerAngles = followedTransform.eulerAngles;
        followedEulerAngles.x = 0;
        followedEulerAngles.z = 0;
        followingTransform.eulerAngles = followOffset + followedEulerAngles;
    }
}