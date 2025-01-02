using System;
using UnityEngine;

public class ToggleableGameObject : MonoBehaviour
{
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}