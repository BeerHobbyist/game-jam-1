using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// ReSharper disable Unity.InefficientPropertyAccess

public class MeleeEnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent _agent;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        if (CheckForLOS())
        {
            _agent.SetDestination(player.position);
        }
    }
    
    // ReSharper disable once InconsistentNaming
    private bool CheckForLOS()
    {
        var direction = player.position - transform.position;
        Physics.Raycast(transform.position, direction, out var hitInfo, 1000f);
        return hitInfo.collider.CompareTag("Player");
    }
}
