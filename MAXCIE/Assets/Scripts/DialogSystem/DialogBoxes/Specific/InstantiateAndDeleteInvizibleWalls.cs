using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAndDeleteInvizibleWalls : InstantiateByCall {
    [SerializeField] GameObject invizibleWalls;

    GameObject[] enemiesInstantiated;

    bool checkIfDead = false;
    void Update()
    {
        if (checkIfDead)
        {
            bool allEnemiesDead = true;

            foreach (GameObject enemy in enemiesInstantiated)
            {
                if (enemy != null) allEnemiesDead = false;
            }

            if (allEnemiesDead) invizibleWalls.SetActive(false);
        }
    }

    protected override void InstantiateEnemy()
    {
        Debug.Log("instanciou");
        enemiesInstantiated = new GameObject[spawnInfo.Length];
        for (int i = 0; i < spawnInfo.Length; i++)
        {
            enemiesInstantiated[i] = Instantiate(spawnInfo[i].enemy, spawnInfo[i].spawnPos.transform.position, spawnInfo[i].enemy.transform.rotation, transform);
        }
        conditionMet = false;
        checkIfDead = true;
    }
}
