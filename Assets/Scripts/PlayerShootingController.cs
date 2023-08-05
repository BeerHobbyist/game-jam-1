using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject gun;
    
    private IProjectileShooting _projectileShooting;
    
    private void Awake()
    {
        _projectileShooting = gun.GetComponent<IProjectileShooting>();
        inputReader.onShootEvent += _projectileShooting.ShootProjectile;
    }
}
