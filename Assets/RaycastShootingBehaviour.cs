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
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private float trailSpeed;
    
    public Vector3 AimPosition { get; set; }

    public void Fire()
    {
        var direction = GetDirection(AimPosition);
        
        
        
        ShootRaycast(direction);
    }

    private void ShootRaycast(Vector3 direction)
    {
        var origin = gunEnd.position;
        Physics.Raycast(origin, direction, out var hit, range);
        SpawnTrail(origin, hit);

        Debug.Log(hit.transform.name);
        Debug.DrawRay(origin, direction * range, Color.red, 1f);
        if (!hit.transform.CompareTag(targetTag))
        {
            var bounceDirection = Vector3.Reflect(direction, hit.normal);
            var bounceOrigin = hit.point;
            Debug.DrawRay(hit.point, bounceDirection * range, Color.blue, 1f);
            Physics.Raycast(hit.point, bounceDirection, out hit, range);
            SpawnTrail(bounceOrigin, hit);
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

    private IEnumerator MoveTrail(Component trail, Vector3 hitPoint)
    {
        var startPosition = trail.transform.position;
        var direction = (hitPoint - startPosition).normalized;
        
        var distance = Vector3.Distance(startPosition, hitPoint);
        var startingDistance = distance;

        while (distance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, 1 - (distance / startingDistance));
            distance -= Time.deltaTime * trailSpeed;
            if (distance < 0)
            {
                yield return new WaitForEndOfFrame();
                trail.transform.position = hitPoint;
            }

            yield return null;
        }
    }

    private void SpawnTrail(Vector3 origin, RaycastHit hit)
    {
        var bulletTrailInstance = Instantiate(bulletTrail, origin, Quaternion.identity);
        StartCoroutine(MoveTrail(bulletTrailInstance, hit.point));
    }
}
