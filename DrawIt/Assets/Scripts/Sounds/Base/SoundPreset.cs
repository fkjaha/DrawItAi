using System;
using UnityEngine;

[Obsolete("Use SoundPresetBase and it's inheritors instead!")]
// [CreateAssetMenu(order = 0, menuName = "Scriptable Objects/Sounds/SoundsPreset", fileName = "Preset_")]
public class SoundPreset : ScriptableObject
{
    public AudioClip GetClip => clip;
    public SoundPresetSettings GetSettings => soundPresetSettings;
    
    [SerializeField] private AudioClip clip;
    [SerializeField] private SoundPresetSettings soundPresetSettings;
}
