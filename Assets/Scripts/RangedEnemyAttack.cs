using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : BaseEnemy
{
    [SerializeField] private GameObject gun;
    [SerializeField] private float attackInterval;
    
    private IProjectileShooting _projectileShooting;

    private float _timeSinceLastAttack;

    private void Awake()
    {
        _projectileShooting = gun.GetComponent<IProjectileShooting>();
    }

    private void Update()
    {
        if (CheckForLOS() && IsReadyToFire())
        {
            _projectileShooting.ShootProjectile();
        }
    }

    private bool IsReadyToFire()
    {
        if(_timeSinceLastAttack > attackInterval)
        {
            _timeSinceLastAttack = 0;
            return true;
        }
        _timeSinceLastAttack += Time.deltaTime;
            return false;
    }
}
