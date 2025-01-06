using UnityEngine;

public class OwnSurfaceSoundPresetContainer : MonoBehaviour
{
    public StepSurfacePreset GetSurfacePreset => surfacePreset;
    
    [SerializeField] private StepSurfacePreset surfacePreset;
}
