using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class PlayerRayCastShootingController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject gun;
    
    private IShootingBehaviour _shootingBehaviour;

    private void Awake()
    {
        _shootingBehaviour = gun.GetComponent<IShootingBehaviour>();
        inputReader.onShootEvent += _shootingBehaviour.Fire;
    }
}
