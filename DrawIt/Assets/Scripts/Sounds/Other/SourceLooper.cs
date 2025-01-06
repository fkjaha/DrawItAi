using System;
using System.Collections;
using UnityEngine;

public class SourceLooper : MonoBehaviour
{
    [SerializeField] private AudioClip clipToLoop;
    [SerializeField] private Vector2 inbetweenRepeatsDelayRange;
    [SerializeField] private Vector2 pitchRange;

    private IEnumerator _loopCoroutine;
    private AudioSource _controlledSource;

    private void Start()
    {
        _controlledSource = GetComponent<AudioSource>();
        _controlledSource.loop = false;
        _controlledSource.clip = clipToLoop;
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
        _controlledSource.pitch = 1 + pitchRange.GetRandomValue();
        _controlledSource.Play();
    }

    private IEnumerator LoopCoroutine()
    {
        while (true)
        {
            PlayLoopCycle();
            yield return new WaitForSeconds(inbetweenRepeatsDelayRange.GetRandomValue()); 
            Debug.Log("Dupa Olega");
        }
    }
}