using UnityEngine;
using UnityEngine.Audio;

public class SoundsPlayer : MonoBehaviour
{
    public static SoundsPlayer Instance;
    
    [SerializeField] private QueuePool<AudioSource> sourcesQueuePool;
    [SerializeField] private AudioMixerGroup defaultGroup;

    private void Awake()
    {
        Instance = this;
    }
    
    public void PlaySound(AudioClip clip, Vector3 position)
    {
        AudioSource source = sourcesQueuePool.GetNextObject();
        source.clip = clip;
        source.transform.position = position;
        source.Play();
    }
    
    public void PlaySound(AudioClip clip)
    {
        PlaySound(clip, Vector3.zero);
    }
    
    public void PlaySound(SoundPresetBase soundPreset, Vector3 position, Transform parent = null)
    {
        if (soundPreset == null)
        {
            Debug.LogWarning("PlaySound called with no soundPreset selected!");
            return;
        }
        AudioSource source = sourcesQueuePool.GetNextObject();

        SoundPresetSettings soundPresetSettings = soundPreset.GetSettings;
        source.volume = soundPresetSettings.GetVolume;
        source.pitch = soundPresetSettings.GetPitch;
        source.spatialBlend = soundPresetSettings.GetSpatialBlend;
        source.outputAudioMixerGroup = soundPresetSettings.GetMixerGroup == null ? defaultGroup : soundPresetSettings.GetMixerGroup;
        
        source.clip = soundPreset.GetClip;
        var sourceTransform = source.transform;
        sourceTransform.position = position;
        sourceTransform.parent = parent;
        
        source.Play();
    }
    
    public void PlaySound(SoundPresetBase soundPreset)
    {
        PlaySound(soundPreset, Vector3.zero);
    }
    
    public void PlaySound(PositionSound positionSound)
    {
        if (positionSound == null)
        {
            Debug.LogWarning("PlaySound called with no positionSound selected!");
            return;
        }
        PlaySound(positionSound.GetPreset, positionSound.GetPlayPosition, positionSound.GetPlayPositionTransform);
    }
}
