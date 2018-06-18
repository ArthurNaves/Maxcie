using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BossStateMachine;
using UnityEngine.AI;

public class Boss : EnemiesBase {
    [SerializeField] GameObject mesh;
    [SerializeField] ParticleSystem[] attacks;

    [SerializeField] float bookAttackCoolDown;
    [SerializeField] float vizibleAttackCoolDown;
    [SerializeField] float distanceToPlayer;
    [SerializeField] float flashingSpeed;
    [SerializeField] int flashingAmmount;

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

    int hp = 6;
    int hpDivided;
    bool invizible = false;

    //protected override void Awake()
    //{
    //    fogOfWarColliders = new List<Collider>();
    //    Player.FogOfWarOffDel += OnFogOfWarOff;
    //    originalSpeed = agent.speed;
    //    currentPath = new NavMeshPath();
    //
    //}
    // Use this for initialization
    protected override void Start () {
        hpDivided = hp / 2;
        base.Start();
        bossStates.InitializeStruct(this, Player.Instance, agent, currentPath, vizibleAttackCoolDown, attacks, bookAttackCoolDown, distanceToPlayer);
        currentState = bossStates.viziCharging;
        currentState.OnStateEnter();
    }
	
	// Update is called once per frame
	void Update () {
        print(currentState);
        Move();
    }

    void Move() { CurrentState.Move(); }
    void Attack() { CurrentState.Attack(); }

    void OnChangingToInvizible()
    {
        ChangeState(StatesTypes.InvMoving);
    }

    void OnDying()
    {

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        CurrentState.OnCollisionEnterEffect(collision);
    }

    protected override void OnParticleCollision(GameObject other)
    {
        CurrentState.OnParticleCollisionEffect();
    }

    //protected override void OnTriggerEnter(Collider other) { if (invizible) base.OnTriggerEnter(other); }
    //protected override void OnTriggerExit(Collider other) { if (invizible) base.OnTriggerExit(other); }
    //protected override void OnFogOfWarOff(Collider fogOfWarCollider) { if(invizible) base.OnFogOfWarOff(fogOfWarCollider); }



    public void DamageBoss()
    {
        if (--hp == hpDivided) OnChangingToInvizible();
        if (hp == 0) OnDying();
    }

    public void ChangeState(StatesTypes state)
    {
        switch (state)
        {
            case StatesTypes.VCharging:
                CurrentState = bossStates.viziCharging;
                break;
            case StatesTypes.VMoving:
                CurrentState = bossStates.viziMoving;
                break;
            case StatesTypes.InvMoving:
                CurrentState = bossStates.invzMoving;
                break;
            case StatesTypes.InvAttacking:
                CurrentState = bossStates.invzAttacking;
                break;
        }
    }

    IEnumerator FlashingRoutine()
    {
        for (int i = 0; i < flashingAmmount; i++)
        {
            yield return new WaitForSeconds(flashingSpeed);
            mesh.SetActive(!mesh.activeSelf);
        }
        mesh.SetActive(true);
    }
}
