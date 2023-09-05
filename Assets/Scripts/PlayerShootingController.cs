using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSo bulletEventChannel;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject gun;
    [SerializeField] private float maxBullets;
    [SerializeField] private UnityEvent onBulletsGone;
    
    private IProjectileShooting _projectileShooting;
    [SerializeField] private float _currentBullets; //testing purposes only

    public float CurrentBullets // TESTING PURPOSES ONLY. USED BY PLACEHOLDER UI. REMOVE LATER!
    {
        get => _currentBullets;
        set => _currentBullets = value;
    }


    private void Awake()
    {
        CurrentBullets = maxBullets;
        _projectileShooting = gun.GetComponent<IProjectileShooting>();
        inputReader.onShootEvent += _projectileShooting.ShootProjectile;
        inputReader.onShootEvent += SpendBullet;
        bulletEventChannel.onEventRaised += AddBullet;
    }

    private void AddBullet()
    {
        CurrentBullets++;
        if (CurrentBullets > maxBullets)
            CurrentBullets = maxBullets;
    }
    
    private void SpendBullet()
    {
        CurrentBullets--;
        if (CurrentBullets > 0) return;
        
        onBulletsGone.Invoke();
        CurrentBullets = 0;
    }
}
