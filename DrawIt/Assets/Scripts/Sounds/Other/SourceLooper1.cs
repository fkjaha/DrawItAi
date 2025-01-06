using System;
using System.Collections;
using UnityEngine;

public class SourceLooper1 : MonoBehaviour
{
    [SerializeField] private SoundPresetRandom soundPreset;
    [SerializeField] private Vector2 inbetweenRepeatsDelayRange;
    [SerializeField] private Transform soundPosition;

    private IEnumerator _loopCoroutine;

    private void Start()
    {
        StartLoop();
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

    private IEnumerator LoopCoroutine()
    {
        while (true)
        {
            PlayLoopCycle();
            yield return new WaitForSeconds(inbetweenRepeatsDelayRange.GetRandomValue());
        }
    }

    private void PlayLoopCycle()
    {
        SoundsPlayer.Instance.PlaySound(soundPreset.GetClip, soundPosition.position);
    }
}