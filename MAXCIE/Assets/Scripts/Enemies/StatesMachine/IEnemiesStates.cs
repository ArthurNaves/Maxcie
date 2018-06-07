using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemiesStates {
    void Move(NavMeshAgent agent, Vector3 playerPos, EnemyBasic enemy, float moveSpeed, float gridLimit);
    void CheckPlayerPosition(Vector3 playerPos, Vector3 enemyPos, EnemiesStates states, EnemyBasic enemy, float minDistToAwake, float minDistToChase);
    void OnStateEnter(NavMeshAgent agent, ref float callsVelocity, float checkPlayrPosTimeAsleep, float originalSpeed);
    void OnStateExit(ref float callsVelocity);
}
//, float minDistToAwake, ref float callsVelocity