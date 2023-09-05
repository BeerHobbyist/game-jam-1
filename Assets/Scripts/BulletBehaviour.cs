using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSo onHit;
    public string TargetTag { get; set; }
    public float Damage { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(TargetTag))
        {
            Destroy(gameObject);
            return;
        }
        if (TargetTag == "Enemy")
        {
            onHit.RaiseEvent();
        }
        other.GetComponent<Health>().TakeDamage(Damage);
        Destroy(gameObject);
    }
}
