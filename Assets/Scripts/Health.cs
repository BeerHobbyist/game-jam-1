using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField]
    protected float MaxHealth { get; set; }
    protected float CurrentHealth { get; set; }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }
    
    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Debug.Log("I'm dead");
        gameObject.SetActive(false);
    }
}
