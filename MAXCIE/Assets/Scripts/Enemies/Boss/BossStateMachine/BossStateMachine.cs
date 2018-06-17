using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

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
    public enum StatesTypes { VCharging, VMoving, InvAttacking, InvMoving }
    //Base---------------------------
    public abstract class StateBase
    {
        protected Boss boss;
        protected Player player;
        protected NavMeshAgent agent;
        protected NavMeshPath currentPath;

        public abstract void PlanNextMove();
        public abstract void OnParticleCollisionEffect();
        public abstract void OnCollisionEnterEffect(Collision collision);
        public abstract void Move();
        public abstract void Attack();
        public abstract void OnStateEnter();
        public abstract void OnStateExit();
    }

    public class ViziCharging : StateBase
    {
        float coolDownTime;

        public ViziCharging(Boss _boss, Player _player, NavMeshAgent _agent, NavMeshPath _currentPath, float _coolDownTimer)
        {
            boss = _boss;
            player = _player;
            agent = _agent;
            currentPath = _currentPath;
            coolDownTime = _coolDownTimer;
        }

        public override void Attack()
        {
        }
        
        public override void Move()
        {
            Vector3 direction = player.transform.position;
            boss.transform.LookAt(new Vector3(direction.x, boss.transform.position.y, direction.z));
        }

        public override void OnCollisionEnterEffect(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player")) player.TakeDamage();
        }

        public override void OnParticleCollisionEffect()
        {
            boss.DamageBoss();
        }

        public override void OnStateEnter()
        {
            agent.isStopped = true;
            boss.StartCoroutine(CooldownTimer());
        }

        public override void OnStateExit()
        {
            agent.isStopped = false;
        }

        public override void PlanNextMove()
        {
        }

        IEnumerator CooldownTimer()
        {
            yield return new WaitForSeconds(coolDownTime);
            boss.ChangeState(StatesTypes.VMoving);
        }
    }

    public class ViziMoving : StateBase
    {
        Vector3 followVector;
        bool moveToPlayer;
        public ViziMoving(Boss _boss, Player _player, NavMeshAgent _agent, NavMeshPath _currentPath)
        {
            boss = _boss;
            player = _player;
            agent = _agent;
            currentPath = _currentPath;
        }

        public override void Attack()
        {
        }

        public override void Move()
        {
            //agent.Move(boss.transform.forward);
            if (Vector3.Distance(boss.transform.position, player.transform.position) <= agent.stoppingDistance) moveToPlayer = false;
            //{
            //    Vector3 direction = player.transform.position;
            //    boss.transform.LookAt(new Vector3(direction.x, boss.transform.position.y, direction.z));
            //    //boss.StopCoroutine(AdjustRotation());
            //Vector3 newDestination = (dir * distanceToPlayer) + player.transform.position;
            //}
            if (moveToPlayer)
            {
                followVector = (player.transform.position - boss.transform.position);
                agent.Move(followVector * 0.01f);
                Vector3 direction = player.transform.position;
                boss.transform.LookAt(new Vector3(direction.x, boss.transform.position.y, direction.z));
            }
            else agent.Move(followVector.normalized * 0.6f);


        }

        public override void OnCollisionEnterEffect(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall")) boss.ChangeState(StatesTypes.VCharging);
            else if (collision.gameObject.CompareTag("Player")) player.TakeDamage();
        }

        public override void OnParticleCollisionEffect()
        {
            boss.DamageBoss();
        }

        public override void OnStateEnter()
        {
            moveToPlayer = true;
        }

        public override void OnStateExit()
        {
        }

        public override void PlanNextMove()
        {
        }
    }

    public class InvzAttacking : StateBase
    {
        ParticleSystem[] attacks;

        float attackChargeTime;
        public InvzAttacking(Boss _boss, Player _player, NavMeshAgent _agent, NavMeshPath _currentPath, ParticleSystem[] _attacks, float _attackChargeTime)
        {
            boss = _boss;
            player = _player;
            agent = _agent;
            currentPath = _currentPath;
            attackChargeTime = _attackChargeTime;
            attacks = _attacks;
        }

        public override void Attack()
        {
            Vector3 lookAtVec = player.transform.position;
            lookAtVec.y = boss.transform.position.y;
            boss.transform.LookAt(lookAtVec);
            attacks[Random.Range(0, attacks.Length - 1)].Play();
            boss.ChangeState(StatesTypes.InvMoving);
        }

        public override void Move()
        {
        }

        public override void OnCollisionEnterEffect(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.Instance.TakeDamage();
                boss.ChangeState(StatesTypes.InvMoving);
            }
        }

        public override void OnParticleCollisionEffect()
        {
            boss.DamageBoss();
            boss.ChangeState(StatesTypes.InvMoving);
        }

        public override void OnStateEnter()
        {
            boss.StartCoroutine(CooldownTimer());
        }

        public override void OnStateExit()
        {
        }

        public override void PlanNextMove()
        {
        }

        IEnumerator CooldownTimer()
        {
            yield return new WaitForSeconds(attackChargeTime);
            Attack();
        }
    }

    public class InvzMoving : StateBase
    {
        float distanceToPlayer;

        public InvzMoving(Boss _boss, Player _player, NavMeshAgent _agent, NavMeshPath _currentPath, float _distanceToPlayer)
        {
            boss = _boss;
            player = _player;
            agent = _agent;
            currentPath = _currentPath;
            distanceToPlayer = _distanceToPlayer;
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void Move()
        {
            if (boss.ArrivedAtDestination()) boss.ChangeState(StatesTypes.InvAttacking);
        }

        public override void OnCollisionEnterEffect(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.Instance.TakeDamage();
                boss.ChangeState(StatesTypes.InvMoving);
            }
        }

        public override void OnParticleCollisionEffect()
        {
            boss.DamageBoss();
        }

        public override void OnStateEnter()
        {
        }

        public override void OnStateExit()
        {
        }

        public override void PlanNextMove()
        {
            Vector3 dir = player.transform.position - boss.transform.position;
            Vector3 newDestination = (dir * distanceToPlayer) + player.transform.position;

            while (true)
            {
                if (boss.ChangeDestination(newDestination)) break;
                else
                {
                    Vector2 tempVector = Utilities.RotatePoint(dir, 10) * distanceToPlayer;
                    newDestination = (new Vector3(tempVector.x, boss.transform.position.y, tempVector.y)) + player.transform.position;
                }
            }

        }
    }

    public struct BossStates
    {
        public ViziCharging viziCharging;
        public ViziMoving viziMoving;
        public InvzAttacking invzAttacking;
        public InvzMoving invzMoving;

        public void InitializeStruct(Boss _boss, Player _player, NavMeshAgent _agent, NavMeshPath _currentPath, float _coolDownTimer, ParticleSystem[] _attacks, float _attackChargeTime, float _distanceToPlayer)
        {
            viziCharging = new ViziCharging(_boss, _player, _agent, _currentPath, _coolDownTimer);
            viziMoving = new ViziMoving(_boss, _player, _agent, _currentPath);
            invzAttacking = new InvzAttacking(_boss, _player, _agent, _currentPath, _attacks, _attackChargeTime);
            invzMoving = new InvzMoving(_boss, _player, _agent, _currentPath, _distanceToPlayer);
        }
    }


}
