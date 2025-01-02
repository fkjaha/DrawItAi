public class PositionFollower : TransformFollower
{
    private protected override void SetAutoOffset()
    {
        followOffset = followingTransform.position - followedTransform.position;
    }

    private protected override void FollowTarget()
    {
        followingTransform.position = followedTransform.position + followOffset;
    }
}