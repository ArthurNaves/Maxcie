using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chasing : IEnemiesStates
{
    public void CheckPlayerPosition(Vector3 playerPos, Vector3 enemyPos, EnemiesStates states, EnemyBasic enemy, float minDistToAwake, float minDistToChase)
    {
        if (Vector3.Distance(playerPos, enemyPos) > minDistToAwake) enemy.ChangeState(states.asleep);
        else if (Vector3.Distance(playerPos, enemyPos) >= minDistToChase) enemy.ChangeState(states.patrolling);
    }

    public void Move(NavMeshAgent agent, Vector3 playerPos, EnemyBasic enemie, float moveSpeed, float gridLimit)
    {
        agent.SetDestination(playerPos);
        //charCtrl.SimpleMove((playerPos-charCtrl.transform.position)*moveSpeed*Time.deltaTime);
    }

    public void OnStateEnter(NavMeshAgent agent, ref float callsVelocity, float checkPlayrPosTimeAsleep, float originalSpeed)
    {
        agent.isStopped = false;
        agent.speed = originalSpeed * 3;
        callsVelocity = Time.fixedDeltaTime;
    }

    public void OnStateExit(ref float callsVelocity)
    {
    }
}
