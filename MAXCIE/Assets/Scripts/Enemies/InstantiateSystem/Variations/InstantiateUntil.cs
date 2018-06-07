using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateUntil : InstantiateByCall {
    [SerializeField] float timeToInstantiate;

    int index = 0;

    protected override void InstantiateEnemy()
    {
        Instantiate(spawnInfo[index].enemy, spawnInfo[index].spawnPos.transform.position, spawnInfo[index].enemy.transform.rotation, transform);
        index++;
        index = index - (index * (index / spawnInfo.Length));//Clampa o valor do index entre 0 e spawnInfo.Length
    }

    public override void CallInstantiator()
    {
        StartCoroutine(CallInstantiatorRotine());
    }

    public virtual void StopInstantiating()
    {
        conditionMet = false;
    }

    IEnumerator CallInstantiatorRotine()
    {
        while (conditionMet)
        {
            InstantiateEnemy();
            yield return new WaitForSeconds(timeToInstantiate);
        }
    }
}
