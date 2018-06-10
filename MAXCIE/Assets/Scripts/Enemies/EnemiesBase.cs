using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemiesBase : FogOfWarObjs {
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator anim;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected string[] tagsOfKiller;

    protected NavMeshPath currentPath;
    protected Player player;

    protected float originalSpeed;


    protected override void Awake()
    {
        base.Awake();
        originalSpeed = agent.speed;
        currentPath = new NavMeshPath();

    }
    protected virtual void Start()
    {
        if (player == null) player = Player.Instance;
    }

    protected virtual void OnParticleCollision(GameObject other)
    {
        if (CompareTags(other.tag))
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 5);
        }
    }
    protected bool CompareTags(string otherTag)
    {
        bool tagEqual = false;
        foreach (string tag in tagsOfKiller) if (otherTag == tag) tagEqual = true;
        return tagEqual;
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.TakeDamage();
            gameObject.SetActive(false);
            Destroy(gameObject, 5);
        }
    }

    public virtual bool ChangeDestination(Vector3 newDest)
    {
        agent.CalculatePath(newDest, currentPath);
        if (currentPath.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetPath(currentPath);
            return true;
        }
        else return false;
    }

    public virtual bool ArrivedAtDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0) return true;
            }
        }
        return false;
    }
    protected void AnimaWalking()
    {
        anim.SetBool("Walking", true);
        anim.SetBool("Running", false);
    }
    protected void AnimaRunning()
    {
        anim.SetBool("Walking", false);
        anim.SetBool("Running", true);
    }
    protected void AnimaDeath()
    {
        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        anim.SetBool("Death", true);
    }
    protected void AnimaIdle()
    {
        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        anim.SetBool("Death", false);
    }
}
