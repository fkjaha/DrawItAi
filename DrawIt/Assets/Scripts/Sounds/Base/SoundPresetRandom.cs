using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Sounds/SoundsPresetRandom", fileName = "RS_")]
public class SoundPresetRandom : SoundPresetBase
{
    public override AudioClip GetClip
    {
        get
        {
            if (dontRepeatBeforeUsingAllClips)
                return GetRandomClipExcludeAllUsedIfPossible();

            if (dontRepeatTwice)
                return GetRandomClipExcludeLast();

            return GetRandomClip();
        }
    }

    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private bool dontRepeatTwice;
    [SerializeField] private bool dontRepeatBeforeUsingAllClips;

    private List<AudioClip> _usedClips;
    private AudioClip _lastUsedClip;

    private void OnEnable()
    {
        _usedClips = new();
        _lastUsedClip = null;
    }

    private AudioClip GetRandomClip()
    {
        if (clips.Count == 0) return default;
        return clips.GetRandomElement();
    }
    
    private AudioClip GetRandomClipExcludeLast()
    {
        AudioClip randomClip = clips.GetRandomElement(_lastUsedClip);
        _lastUsedClip = randomClip;
        
        return randomClip;
    }

    private AudioClip GetRandomClipExcludeAllUsedIfPossible()
    {
        ReInitializeUsedClipsIfNeeded();
        
        AudioClip randomClip = clips.GetRandomElement(_usedClips);
        _usedClips.Add(randomClip);
        
        return randomClip;
    }

    private void ReInitializeUsedClipsIfNeeded()
    {
        if (_usedClips == null || _usedClips.Count == clips.Count)
            _usedClips = new();
    }
}
