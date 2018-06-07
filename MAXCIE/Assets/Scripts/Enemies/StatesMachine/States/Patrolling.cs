using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : IEnemiesStates
{
    public void CheckPlayerPosition(Vector3 playerPos, Vector3 enemyPos, EnemiesStates states, EnemyBasic enemy, float minDistToAwake, float minDistToChase)
    {
        if (Vector3.Distance(playerPos, enemyPos) > minDistToAwake) enemy.ChangeState(states.asleep);
        else if (Vector3.Distance(playerPos, enemyPos) < minDistToChase) enemy.ChangeState(states.chasing);
    }

    public void Move(NavMeshAgent agent, Vector3 playerPos, EnemyBasic enemy, float moveSpeed, float gridLimit)
    {
        bool pathSuccefull = false;
        if (enemy.ArrivedAtDestination() || !agent.hasPath)
        {
            do
            {
                pathSuccefull = enemy.ChangeDestination(new Vector3(Random.Range(-gridLimit, gridLimit), 0, Random.Range(-gridLimit, gridLimit)));
            } while (!pathSuccefull);
        }
        
    }

    public void OnStateEnter(NavMeshAgent agent, ref float callsVelocity, float checkPlayrPosTimeAsleep, float originalSpeed)
    {
        agent.isStopped = false;
        agent.speed = originalSpeed;
        callsVelocity = Time.fixedDeltaTime;
    }

    public void OnStateExit(ref float callsVelocity)
    {
        
    }
}
