using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// ReSharper disable Unity.InefficientPropertyAccess

/// <summary>
/// This class is a class for keeping all the common functionality between the enemy controllers.
/// </summary>
public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected Transform player;
    
    protected NavMeshAgent agent;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    // ReSharper disable once InconsistentNaming
    protected bool CheckForLOS()
    {
        var direction = player.position - transform.position;
        Physics.Raycast(transform.position, direction, out var hitInfo, 1000f);
        return hitInfo.collider.CompareTag("Player");
    }
}
