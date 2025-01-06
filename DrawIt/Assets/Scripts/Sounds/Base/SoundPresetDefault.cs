using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Sounds/SoundsPresetDefault", fileName = "S_")]
public class SoundPresetDefault : SoundPresetBase
{
    public override AudioClip GetClip => clip;

    [SerializeField] private AudioClip clip;
}
