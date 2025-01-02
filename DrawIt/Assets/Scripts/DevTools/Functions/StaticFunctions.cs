using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public static class StaticFunctions
{
    public static Vector3 Vector2ToVector3XZ(this Vector2 inputVector)
    {
        return new Vector3(inputVector.x, 0f, inputVector.y);
    }
    
    public static Vector3 Vector2ToVector3XZInversed(this Vector2 inputVector)
    {
        return new Vector3(inputVector.y, 0f, inputVector.x);
    }
    
    public static Vector2 Vector3XZToVector2(this Vector3 inputVector)
    {
        return new Vector2(inputVector.x, inputVector.z);
    }
    
    public static Vector3 Vector3ToFlat(this Vector3 inputVector)
    {
        return new Vector3(inputVector.x, 0f, inputVector.z);
    }

    public static Vector3 XZero(this Vector3 inputVector)
    {
        return new Vector3(0f, inputVector.y, inputVector.z);
    }

    public static Vector3 YZero(this Vector3 inputVector)
    {
        return new Vector3(inputVector.x, 0f, inputVector.z);
    }

    public static Vector3 ZZero(this Vector3 inputVector)
    {
        return new Vector3(inputVector.x, inputVector.y, 0f);
    }

    public static float XZDistance(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a.Vector3ToFlat(), b.Vector3ToFlat());
    }
    
    public static T GetRandomElement<T>(this List<T> inputList)
    {
        return inputList[Random.Range(0, inputList.Count)];
    }
    
    public static T GetRandomElement<T>(this IReadOnlyList<T> inputList)
    {
        return inputList[Random.Range(0, inputList.Count)];
    }
    
    public static T GetRandomElementComparable<T>(this IReadOnlyList<T> inputList, T exclude) where T: IComparable
    {
        inputList = inputList.Where(element => !element.Equals(exclude)).ToList();
        return inputList[Random.Range(0, inputList.Count)];
    }
    
    public static T GetRandomElement<T>(this IReadOnlyList<T> inputList, T exclude) where T: class
    {
        inputList = inputList.Where(element => element != exclude).ToList();
        return inputList[Random.Range(0, inputList.Count)];
    }
    
    public static T GetRandomElement<T>(this IReadOnlyList<T> inputList, List<T> exclude)
    {
        inputList = inputList.Where(element => !exclude.Contains(element)).ToList();
        return inputList[Random.Range(0, inputList.Count)];
    }
    
    public static float GetRandomValue(this Vector2 inputRange)
    {
        return Random.Range(inputRange.x, inputRange.y);
    }
    
    public static int GetRandomValue(this Vector2Int inputRange)
    {
        return Random.Range(inputRange.x, inputRange.y + 1);
    }

    public static T CopyClassByJsonSerialization<T>(this T instanceToCopy)
    {
        return JsonUtility.FromJson<T>
            (JsonUtility.ToJson(instanceToCopy));
    }
    
    public static int T1F_1(this bool inputBool)
    {
        return inputBool ? 1 : -1;
    }
    
    public static bool T1F_1Reversed(this int inputInt)
    {
        return inputInt == 1;
    }
    
    public static int T_1F1(this bool inputBool)
    {
        return inputBool ? -1 : 1;
    }

    public static bool T_1F1Reversed(this int inputInt)
    {
        return inputInt == -1;
    }
    
    public static int T1F0(this bool inputBool)
    {
        return inputBool ? 1 : 0;
    }
    
    public static bool T1F0Reversed(this int inputInt)
    {
        return inputInt == 1;
    }
    
    public static int T0F1(this bool inputBool)
    {
        return inputBool ? 0 : 1;
    }

    public static bool T0F1Reversed(this int inputInt)
    {
        return inputInt == 0;
    }

    public static float VolumePercentInDecibel(this float volume, float multiplyFactor = 20)
    {
        return Mathf.Log10(volume) * multiplyFactor;
    }
    
    public static float InverseVolumePercentInDecibel(this float decibelsVolume, float multiplyFactor = 20)
    {
        return Mathf.Pow(10, decibelsVolume/multiplyFactor);
    }

    public static void ExecuteIfButtonClicked(string buttonName, UnityAction action)
    {
        if(GUILayout.Button(buttonName)) action?.Invoke();
    }

    public static EventTrigger.Entry GetEntryForEvent(this EventTriggerType triggerType, UnityAction<PointerEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = triggerType
        };
        entry.callback.AddListener(data => action.Invoke((PointerEventData)data));
        return entry;
    }

    public static void SetActive(this List<GameObject> targetList, bool activeState)
    {
        foreach (GameObject gameObject in targetList)
        {
            gameObject.SetActive(activeState);
        }
    }

    public static int Clamp(this int inputInt, int minValue, int maxValue)
    {
        return Mathf.Clamp(inputInt, minValue, maxValue);
    }
    
    public static float Clamp(this float inputInt, float minValue, float maxValue)
    {
        return Mathf.Clamp(inputInt, minValue, maxValue);
    }
    
    public static bool Includes(this LayerMask layerMask, int layer)
    {
        // return (layer & layerMask) != 0;
        return layerMask == (layerMask | (1 << layer));
    }
    
    public static string SecondsToMinutesSecondsTimerView(this int seconds)
    {
        return $"{seconds / 60:D2}:{seconds % 60:D2}";
    }
    
    public static string SecondsToMinutesSecondsTimerViewWithLetters(this int seconds)
    {
        return $"{seconds / 60:D2}m {seconds % 60:D2}s";
    }
}
