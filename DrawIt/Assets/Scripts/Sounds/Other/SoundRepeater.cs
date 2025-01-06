using System;
using System.Collections;
using UnityEngine;

public class SoundRepeater : MonoBehaviour
{
    [SerializeField] private SoundPresetBase soundPreset;
    [SerializeField] private Vector2 inBetweenRepeatsDelayRange;
    [SerializeField] private bool playOnStart;

    private IEnumerator _loopCoroutine;
    private AudioSource _controlledSource;

    public void Start()
    {
        if (playOnStart)
        {
            StartLoop();
        }
    }

    public void StartLoop()
    {
        StopLoop();

        _loopCoroutine = LoopCoroutine();
        StartCoroutine(_loopCoroutine);
    }

    public void StopLoop()
    {
        if (_loopCoroutine == null) return;
        
        StopCoroutine(_loopCoroutine);
    }

    private void PlayLoopCycle()
    {
        SoundsPlayer.Instance.PlaySound(soundPreset, transform.position);
    }

    private IEnumerator LoopCoroutine()
    {
        while (true)
        {
            PlayLoopCycle();
            yield return new WaitForSeconds(inBetweenRepeatsDelayRange.GetRandomValue());
        }
    }
}