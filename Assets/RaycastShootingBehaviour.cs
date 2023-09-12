using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RaycastShootingBehaviour : MonoBehaviour, IShootingBehaviour
{
    [SerializeField] private Transform gunEnd;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float bounceRange = 100f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private string targetTag = "Enemy";
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private float trailSpeed;
    
    public Vector3 AimPosition { get; set; }
    
    private float _timeSinceLastShot;
    
    private bool _isReadyToFire;

    private void Update()
    {
        _isReadyToFire = CheckIsIsReadyToFire();
    }

    public void Shoot()
    {
        if (!_isReadyToFire)
            return;
        
        var direction = GetDirection(AimPosition);
        
        var hits = FireBullet(direction);
        StartCoroutine(DrawTrail(hits));
    }

    private List<RaycastHit> FireBullet(Vector3 direction)
    {
        var hits = new List<RaycastHit>();
        var origin = gunEnd.position;
        var hit = ShootRaycast(origin, direction);
        hits.Add(hit);
        var distance = 0f;
        
        var bulletTrailInstance = Instantiate(bulletTrail, origin, Quaternion.identity);
        
        while (distance < bounceRange)
        {
            origin = hit.point;
            var lastDirection = direction;
            direction = Vector3.Reflect(lastDirection, hit.normal);
            hit = ShootRaycast(origin, direction);
            hits.Add(hit);
            distance += Vector3.Distance(origin, hit.point);
        }

        return hits;
    }
    
    private void DealDamage(RaycastHit hit)
    {
        var health = hit.transform.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
    
    private RaycastHit ShootRaycast(Vector3 origin, Vector3 direction)
    {
        Physics.Raycast(origin, direction, out var hit);
        Debug.DrawRay(origin, direction * hit.distance, Color.red, 1f);
        
        if (!hit.transform.CompareTag(targetTag))
            return hit;
        
        DealDamage(hit);
        return hit;
    }
    
    private Vector3 GetDirection(Vector3 target)
    {
        var dir = (target - gunEnd.position).normalized;
        dir.y = 0;
        return dir;
    }

    private IEnumerator LerpTrail(Component trail, Vector3 hitPoint)
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
    
    private IEnumerator DrawTrail(List<RaycastHit> hits)
    {
        var trail = Instantiate(bulletTrail, gunEnd.position, Quaternion.identity);
        foreach (var hit in hits)
        {
            var lerpTrailCoroutine = LerpTrail(trail, hit.point);
            StartCoroutine(LerpTrail(trail, hit.point));
            while (lerpTrailCoroutine.MoveNext())
            {
                yield return null;
            }
        }
    }

    private bool CheckIsIsReadyToFire()
    {
        if(_timeSinceLastShot > 1 / fireRate)
        {
            _timeSinceLastShot = 0;
            return true;
        }
        _timeSinceLastShot += Time.deltaTime;
        return false;
    }
}
