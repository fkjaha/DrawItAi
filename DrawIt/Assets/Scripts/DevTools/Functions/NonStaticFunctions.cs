using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NonStaticFunctions : MonoBehaviour
{
    public static NonStaticFunctions Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DoAfterTime(float time, UnityAction doAction)
    {
        StartCoroutine(DoAfterTimeCoroutine(time, doAction));
    }
    
    public IEnumerator DoAfterTimeWithCoroutine(float time, UnityAction doAction)
    {
        IEnumerator coroutine = DoAfterTimeCoroutine(time, doAction);
        StartCoroutine(coroutine);
        return coroutine;
    }

    public void DoStopCoroutine(IEnumerator targetCoroutine)
    {
        if(targetCoroutine == null) return;
        
        StopCoroutine(targetCoroutine);
    }

    private IEnumerator DoAfterTimeCoroutine(float time, UnityAction doAction)
    {
        yield return new WaitForSeconds(time);
        doAction?.Invoke();
    }
}