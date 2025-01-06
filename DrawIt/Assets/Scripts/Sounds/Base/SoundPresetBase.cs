using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public abstract class SoundPresetBase : ScriptableObject
{
    public abstract AudioClip GetClip { get; }
    public SoundPresetSettings GetSettings => soundPresetSettings;
    
    [SerializeField] private SoundPresetSettings soundPresetSettings;

    public void TryPlay()
    {
        if (SoundsPlayer.Instance == null)
        {
            Debug.LogWarning("Playing attempt failed! SoundsPlayer instance doesnt exist!", this);
            return;
        }
        
        SoundsPlayer.Instance.PlaySound(this);
    }
}

[Serializable]
public class SoundPresetSettings
{
    public AudioMixerGroup GetMixerGroup => mixerGroup;
    public float GetVolume => volume;
    public float GetSpatialBlend => spatialBlend;

    public float GetPitch
    {
        get
        {
            if (randomizePitch)
            {
                return pitch + Random.Range(randomizePitchBounds.x, randomizePitchBounds.y);
            }
            return pitch;
        }
    }

    [SerializeField] private AudioMixerGroup mixerGroup;

    [Range(0,1)]
    [SerializeField] private float volume;

    [Range(0,1)]
    [SerializeField] private float spatialBlend;

    [Range(-3,3)]
    [SerializeField] private float pitch;

    [SerializeField] private bool randomizePitch;
    [SerializeField] private Vector2 randomizePitchBounds;

    public SoundPresetSettings(float volumeTarget, float spatialBlendTarget, float pitchTarget)
    {
        volume = volumeTarget;
        spatialBlend = spatialBlendTarget;
        pitch = pitchTarget;
    }
    
    public SoundPresetSettings()
    {
        volume = 1;
        spatialBlend = 0;
        pitch = 1;
    }
}
