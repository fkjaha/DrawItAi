using UnityEngine;

public class ChangePositionOnStart : MonoBehaviour
{
    [SerializeField] private Transform transformToMove;
    [SerializeField] private Vector3 offsetPosition;
    [Space(20f)]
    [SerializeField] private bool useOutsideEditor;

    private void Start()
    {
#if UNITY_EDITOR
        transformToMove.position = transform.position + offsetPosition;
#endif
        
#if !UNITY_EDITOR
        if(useOutsideEditor) transformToMove.position = transform.position + offsetPosition;
#endif
    }
}