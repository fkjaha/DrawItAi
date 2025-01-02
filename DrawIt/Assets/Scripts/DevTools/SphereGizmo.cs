using UnityEngine;

public class SphereGizmo : MonoBehaviour
{
    [SerializeField] private float radius = 0.2f;
    [SerializeField] private Color color = Color.green;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}