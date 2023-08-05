using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;

    private Health _targetHealth;
    private float _nextAttackTime;
    
    private void Awake()
    {
        _targetHealth = target.GetComponent<Health>();
    }
    
    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Vector3.Distance(transform.position, target.position) <= attackRange && _nextAttackTime > attackRate)
        {
            _nextAttackTime = 0;
            _targetHealth.TakeDamage(damage);
        }
        else
        {
            _nextAttackTime += Time.deltaTime;
        }
    }
}
