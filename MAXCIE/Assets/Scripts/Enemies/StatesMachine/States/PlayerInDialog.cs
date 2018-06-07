using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInDialog : IEnemiesStates
{
    public void CheckPlayerPosition(Vector3 playerPos, Vector3 enemyPos, EnemiesStates states, EnemyBasic enemy, float minDistToAwake, float minDistToChase)
    {
    }

    public void Move(NavMeshAgent agent, Vector3 playerPos, EnemyBasic enemy, float moveSpeed, float gridLimit)
    {
    }

    public void OnStateEnter(NavMeshAgent agent, ref float callsVelocity, float checkPlayrPosTimeAsleep, float originalSpeed)
    {
        if(agent!=null && agent.isActiveAndEnabled && agent.isOnNavMesh) agent.isStopped = true;
    }

    public void OnStateExit(ref float callsVelocity)
    {
    }
}
