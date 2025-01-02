// using UnityEngine;
//
// public class KillZone : MonoBehaviour
// {
//     [SerializeField] private bool debugCollisions;
//     
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.TryGetComponent(out IDamageable damagable))
//         {
//             if (debugCollisions) Debug.Log(damagable.GetDamageableObject().name);
//             damagable.Kill(true);
//         }
//     }
//
//     private void OnCollisionEnter(Collision collision)
//     {
//         if (collision.gameObject.TryGetComponent(out IDamageable damagable))
//         {
//             if (debugCollisions) Debug.Log(damagable.GetDamageableObject().name);
//             damagable.Kill(true);
//         }
//     }
// }