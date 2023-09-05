using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// ReSharper disable Unity.InefficientPropertyAccess

public class MeleeEnemy : BaseEnemy
{ 
    private void Update()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        if (CheckForLOS())
        {
            agent.SetDestination(player.position);
        }
    }
}
