using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShootingBehaviour : MonoBehaviour, IShootingBehaviour
{
    [SerializeField] private Transform gunEnd;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private string targetTag = "Enemy";
    
    public Vector3 AimPosition { get; set; }

    public void Fire()
    {
        var direction = GetDirection(AimPosition);
        ShootRaycast(direction);
    }

    private void ShootRaycast(Vector3 direction)
    {
        Physics.Raycast(gunEnd.position, direction, out var hit, range);
        Debug.Log(hit.transform.name);
        Debug.DrawRay(gunEnd.position, direction * range, Color.red, 1f);
        if (!hit.transform.CompareTag(targetTag))
        {
            var bounceDirection = Vector3.Reflect(direction, hit.normal);
            Debug.DrawRay(hit.point, bounceDirection * range, Color.blue, 1f);
            Physics.Raycast(hit.point, bounceDirection, out hit, range);
            return;
        }

        var health = hit.transform.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
    
    private Vector3 GetDirection(Vector3 target)
    {
        var dir = (target - gunEnd.position).normalized;
        dir.y = 0;
        return dir;
    }
}
