using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInstantiateByCall : InstantiateByCall {
    static int index;
    [SerializeField] SpawnInfo[] spawnInfo2;
    [SerializeField] SpawnInfo[] spawnInfo3;

    protected override void InstantiateEnemy()
    {
        conditionMet = false;
        switch (index++)
        {
            case 0:
                foreach (SpawnInfo info in spawnInfo)
                {
                    Instantiate(info.enemy, info.spawnPos.transform.position, info.enemy.transform.rotation, transform);
                }
                break;
            case 1:
                foreach (SpawnInfo info in spawnInfo2)
                {
                    Instantiate(info.enemy, info.spawnPos.transform.position, info.enemy.transform.rotation, transform);
                }
                break;
            case 2:
                foreach (SpawnInfo info in spawnInfo3)
                {
                    Instantiate(info.enemy, info.spawnPos.transform.position, info.enemy.transform.rotation, transform);
                }
                break;
        }

    }
}
