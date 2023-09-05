using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : BaseEnemy
{
    private void TrackPlayer()
    {
        if(!CheckForLOS())
            return;
        var position = player.position;
        transform.rotation = Quaternion.LookRotation(position - transform.position);
        agent.SetDestination(position);
    }

    private void Update()
    {
        TrackPlayer();
    }
}
