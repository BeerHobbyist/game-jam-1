using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : BaseEnemy
{
    [SerializeField] private GameObject gun;
    [SerializeField] private float attackInterval;
    
    private IShootingBehaviour _projectileShooting;

    private float _timeSinceLastAttack;

    private void Awake()
    {
        _projectileShooting = gun.GetComponent<IShootingBehaviour>();
    }

    private void Update()
    {
        if (CheckForLOS() && IsReadyToFire())
        {
            _projectileShooting.Shoot();
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
