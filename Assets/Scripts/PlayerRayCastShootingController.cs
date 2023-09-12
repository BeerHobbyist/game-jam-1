using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class PlayerRayCastShootingController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    private IShootingBehaviour _shootingBehaviour;

    private void Awake()
    {
        _shootingBehaviour = GetComponent<IShootingBehaviour>();
        inputReader.onShootEvent += _shootingBehaviour.Shoot;
    }
}
