using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class ProjectileShooting : MonoBehaviour, IShootingBehaviour
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float damage;
    [SerializeField] private string targetTag;
    
    public float ProjectileSpeed { get; set; }

    private void Awake()
    {
        ProjectileSpeed = projectileSpeed;
    }

    public void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
        projectile.GetComponent<BulletBehaviour>().Damage = damage;
        projectile.GetComponent<BulletBehaviour>().TargetTag = targetTag;
        var projectileRb = projectile.GetComponent<Rigidbody>();
        // gun is rotated 90 degrees on the x axis so we need to use right instead of forward. Probably temporary
        projectileRb.AddForce(projectile.transform.right * projectileSpeed, ForceMode.Impulse);
        
        Destroy(projectile, 5f);
    }
}
