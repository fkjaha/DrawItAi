using UnityEngine;

public class PositionSound : MonoBehaviour
{
    public Transform GetPlayPositionTransform => connectSoundToTransform ?
        (soundPositionTransform ? soundPositionTransform : transform) 
        : null;
    public Vector3 GetPlayPosition => soundPositionTransform ? soundPositionTransform.position : transform.position;
    public SoundPresetBase GetPreset => soundPreset;
    
    [Header("Main")]
    [SerializeField] private SoundPresetBase soundPreset;
    [SerializeField] private bool connectSoundToTransform;
    
    [Header("Optional")]
    [SerializeField] private Transform soundPositionTransform;

    public void TryPlay()
    {
        SoundsPlayer.Instance.PlaySound(this);
    }
}
