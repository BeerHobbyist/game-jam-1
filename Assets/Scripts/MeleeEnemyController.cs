using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        _agent.SetDestination(player.position);
    }
}
