// using System;
// using Drawing;
// using UnityEngine;
//
// public class BetterBoxColliderGizmos : MonoBehaviour
// {
//     [Range(0, 1)]
//     [SerializeField] private float transparency = .22f;
//     [SerializeField] private Color color = Color.green;
//     [SerializeField] private bool alwaysDraw;
//
//     [HideInInspector]
//     [SerializeField] private BoxCollider _boxCollider;
//
//     private void Reset()
//     {
//         _boxCollider = GetComponent<BoxCollider>();
//     }
//
//     private void OnValidate()
//     {
//         _boxCollider = GetComponent<BoxCollider>();
//     }
//
//     public override void DrawGizmos()
//     {
//         if (!GizmoContext.InSelection(this) && !alwaysDraw) return;
//         if (_boxCollider == null) return;
//
//         Vector3 scale = new Vector3(_boxCollider.size.x * transform.lossyScale.x,
//             _boxCollider.size.y * transform.lossyScale.y, _boxCollider.size.z * transform.lossyScale.z);
//         Vector3 centerPosition = new Vector3(_boxCollider.center.x * transform.lossyScale.x,
//             _boxCollider.center.y * transform.lossyScale.y, _boxCollider.center.z * transform.lossyScale.z);
//         
//         color.a = transparency;
//         
//         Draw.SolidBox(transform.position + centerPosition, transform.rotation, scale, color);
//     }
// }