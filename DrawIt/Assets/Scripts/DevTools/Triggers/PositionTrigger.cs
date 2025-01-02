using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PositionTrigger : MonoBehaviour
{
    public event UnityAction OnTriggered;

    [SerializeField] private bool triggerMultipleTimes;
    [SerializeField] private UnityEvent onTriggered;
    [SerializeField] private bool disableOnTrigger;
    [SerializeField] private Transform originTransform;
    [SerializeField] private Transform objectTransform;
    [SerializeField] private bool3 axisToCount;
    [SerializeField] private Vector3 triggerPositionDifference;
    [SerializeField] private bool greaterThan;
    [Space(20f)]
    [SerializeField] private float gizmoArrowSizeMultiplier;

    private bool _wasTriggered;
    
    private void Update()
    {
        if(_wasTriggered && !triggerMultipleTimes) return;

        Vector3 currentPositionDifference = objectTransform.position - originTransform.position;
        if (axisToCount.x)
        {
            TriggerIfDifferenceIsAccording(currentPositionDifference.x, triggerPositionDifference.x);
        }
        else if (axisToCount.y)
        {
            TriggerIfDifferenceIsAccording(currentPositionDifference.y, triggerPositionDifference.y);
        }
        else if (axisToCount.z)
        {
            TriggerIfDifferenceIsAccording(currentPositionDifference.z, triggerPositionDifference.z);
        }
    }

    private void TriggerIfDifferenceIsAccording(float difference, float expectedDifference)
    {
        bool shouldBeGreaterAndItIs = greaterThan && difference >= expectedDifference;
        bool shouldBeLessAndItIs = !greaterThan && difference <= expectedDifference;

        if(shouldBeGreaterAndItIs || shouldBeLessAndItIs) Trigger();
    }

    private void Trigger()
    {
        if(_wasTriggered && !triggerMultipleTimes) return;
        
        OnTriggered?.Invoke();
        onTriggered.Invoke();
        _wasTriggered = true;

        if (disableOnTrigger) gameObject.SetActive(false);
    }
}