using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInstantiatorBase : MonoBehaviour {
    [System.Serializable] protected struct SpawnInfo
    {
        public GameObject spawnPos;
        public GameObject enemy;
    }

    [SerializeField] protected SpawnInfo[] spawnInfo;

    [SerializeField] protected bool conditionMet;
 	// Use this for initialization
	
    protected virtual void InstantiateEnemy()
    {
        conditionMet = false;
        foreach (SpawnInfo info in spawnInfo)
        {
            Instantiate(info.enemy, info.spawnPos.transform.position, info.enemy.transform.rotation, transform);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(conditionMet) InstantiateEnemy();
    }
}
