using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateByCall : EnemiesInstantiatorBase {

	// Use this for initialization
	protected virtual void Start () {
        conditionMet = true;
	}
    public virtual void CallInstantiator()
    {
        InstantiateEnemy();
    }
    protected override void OnTriggerEnter(Collider other)
    {
    }
}
