using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private float maxHealth = 100f;

    private void Awake()
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
    }
}
