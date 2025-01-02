using UnityEngine;

public abstract class ValueProcessor<T> : MonoBehaviour
{
    public abstract T ProcessValue(T inputValue);
}