using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Asleep : IEnemiesStates
{
    public void CheckPlayerPosition(Vector3 playerPos, Vector3 enemiePos, EnemiesStates states, EnemyBasic enemie, float minDistToAwake, float minDistToChase)
    {
        if (Vector3.Distance(playerPos, enemiePos) <= minDistToAwake) enemie.ChangeState(states.patrolling);
        else if (Vector3.Distance(playerPos, enemiePos) < minDistToChase) enemie.ChangeState(states.chasing);
    } 

    public void Move(NavMeshAgent agent, Vector3 playerPos, EnemyBasic enemie, float moveSpeed, float gridLimit)
    {
    }

    public void OnStateEnter(NavMeshAgent agent, ref float callsVelocity, float checkPlayrPosTimeAsleep, float originalSpeed)
    {
        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh) agent.isStopped = true;
        callsVelocity = checkPlayrPosTimeAsleep;
    }

    public void OnStateExit(ref float callsVelocity)
    {
        
    }
}
