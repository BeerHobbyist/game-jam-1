using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public string TargetTag { get; set; }
    public float Damage { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(TargetTag))
        {
            Destroy(gameObject);
            return;
        }
        
        other.GetComponent<Health>().TakeDamage(Damage);
        Destroy(gameObject);
    }
}
