using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasic : EnemiesBase {
    public IEnemiesStates CurrentState
    {
        get { return currentState; }
        set
        {
            currentState.OnStateExit(ref checkPlayrPosTime);
            currentState = value;
            currentState.OnStateEnter(agent, ref checkPlayrPosTime, checkPlayrPosTimeAsleep, originalSpeed);
        }
    }
    IEnemiesStates currentState;
    EnemiesStates states;

    Vector3 patrolDestination;

    

    [SerializeField] float minDistToAwake;
    [SerializeField] float checkPlayrPosTimeAsleep;
    [SerializeField] float minDistToChase;
    float checkPlayrPosTime;

    //a.0.1

    protected override void Awake()
    {
        states.EnemiesStatesInitializer();
        checkPlayrPosTime = checkPlayrPosTimeAsleep;
        base.Awake();
    }

    protected override void Start()
    {
        DialogBoxBase.dialogEvent += ONdialogEvent;
        base.Start();
        currentState = states.asleep;
        StartCoroutine("PlayerPosCorroutine");
    }

    void FixedUpdate()
    {
        Move();
        AnimaRunning();
    }

    void Move()
    {
        CurrentState.Move(agent, player.transform.position, this, moveSpeed, 99);
    }

    void CheckPlayerPosition()
    {

        CurrentState.CheckPlayerPosition(player.transform.position, transform.position,  states, this, minDistToAwake, minDistToChase);
    }
    void ONdialogEvent()
    {
        if (CurrentState == states.inDialog) ChangeState(states.asleep);
        else ChangeState(states.inDialog);
    }

    public void ChangeState(IEnemiesStates newState)
    {
        CurrentState = newState;
    }

    IEnumerator PlayerPosCorroutine()
    {
        while (true)
        {
            CheckPlayerPosition();
            yield return new WaitForSeconds(checkPlayrPosTime);
        }
    }

}
