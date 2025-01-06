using UnityEngine;

public class StepSurfaceRaycaster : MonoBehaviour
{
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private StepSurfacePreset defaultStepSurfacePreset;

    public StepSurfacePreset GetCurrentStepSurfacePreset()
    {
        if (Physics.Raycast(raycastPoint.position, Vector3.down, out RaycastHit hit, raycastDistance, raycastMask))
        {
            if (hit.collider.gameObject.TryGetComponent(
                    out OwnSurfaceSoundPresetContainer ownSurfaceSoundPresetContainer))
            {
                return ownSurfaceSoundPresetContainer.GetSurfacePreset;
            }

            return defaultStepSurfacePreset;
        }
        return null;
    }
}
