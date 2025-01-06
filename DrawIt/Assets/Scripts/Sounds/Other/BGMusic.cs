using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMusic : MonoBehaviour
{
    private AudioSource _source;
    
    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void ChangeBGMusic(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }
}
