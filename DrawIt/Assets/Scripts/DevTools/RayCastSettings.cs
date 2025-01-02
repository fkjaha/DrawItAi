using System;
using UnityEngine;

[Serializable]
public struct RayCastSettings
{
    public float GetDistance => distance;
    public LayerMask GetLayerMask => layerMask;

    [SerializeField] private Transform originTransform;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layerMask;
    
    public Ray GetRay()
    {
        return new Ray(originTransform.position, originTransform.forward);
    }
}