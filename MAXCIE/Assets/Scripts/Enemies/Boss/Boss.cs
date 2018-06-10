using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BossStateMachine;

public class Boss : EnemiesBase {
    BossStates bossStates;

    protected override void Awake()
    {
        base.Awake();
    }
    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
