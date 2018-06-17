using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

namespace BossStateMachine
{
    /*
     * O boss aparece primeiro idle, 
     * acontece uma série de eventos
     * entra para o modo moving, 
     * fica dibrando o jogador até atacar ela,
     * 2 estados principais:
     * invizivel
     * rapidao
     */
    public enum StatesTypes { }
    //Base---------------------------
    public abstract class StateBase
    {
        protected Boss boss;
        protected Player player;
        protected NavMeshAgent agent;
        protected NavMeshPath currentPath;

        public abstract bool PlanNextMove();
        public abstract void OnParticleCollisionEffect();
        public abstract bool Move();
        public abstract void Attack();
        public abstract void OnStateEnter();
        public abstract void OnStateExit();
    }
    //voando reto--------------------
    public abstract class VizibleStateBase : StateBase
    {
        
    }

    public class ViziAttacking : VizibleStateBase
    {
        public ViziAttacking(Boss _boss, Player _player, NavMeshAgent _agent, NavMeshPath _currentPath)
        {
            boss = _boss;
            player = _player;
            agent = _agent;
            currentPath = _currentPath;
        }

        public override void Attack()
        {
        }

        public override bool Move()
        {
        }

        public override void OnParticleCollisionEffect()
        {
        }

        public override void OnStateEnter()
        {
        }

        public override void OnStateExit()
        {
        }

        public override bool PlanNextMove()
        {
        }
    }

    public class ViziCharging : VizibleStateBase
    {
        float coolDownTime;
    }

    public class ViziMoving : VizibleStateBase
    {

    }

    public class ViziTakingDamage : VizibleStateBase
    {

    }

    //Invizible----------------------
    public abstract class InvzStateBase : StateBase
    {

    }

    public class InvzAttacking : InvzStateBase
    {

    }

    public class InvzIdle : InvzStateBase
    {

    }

    public class InvzMoving : InvzStateBase
    {

    }

    public class InvzTakingDamage : InvzStateBase
    {

    }


    public struct BossStates
    {
        
    }
}
