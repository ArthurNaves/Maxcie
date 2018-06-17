using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BossStateMachine;

public class Boss : EnemiesBase {
    BossStates bossStates;
    StateBase currentState;
    StateBase CurrentState
    {
        get { return currentState; }
        set
        {
            currentState.OnStateExit();
            currentState = value;
            currentState.OnStateEnter();
        }
    }

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

    void Move() { CurrentState.Move(); }
    void Attack() { CurrentState.Attack(); }

    protected override void OnCollisionEnter(Collision collision)
    {
        CurrentState.OnParticleCollisionEffect();
    }

}
