using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for all enemy AI
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public float speed = 1.0f;
    [SerializeField]
    private float attackDist = 1.8f;
    protected float AttackDist { get { return attackDist; } }
    [SerializeField]
    protected float timeBetweenAttack = 1.0f;
    public TargetType MainTarget = TargetType.EnemyTarget;
    public AIState state = AIState.Idle;

    protected NavMeshAgent nma;
    protected EnemyScript es;

    protected Vector3 currentTarget;
    private float attackDistSqr;
    protected float AttackDistSqr { get { return attackDistSqr; } }

    protected void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        es = GetComponent<EnemyScript>();
        attackDistSqr = attackDist * attackDist;
    }

    protected void Start()
    {
        nma.speed = speed;
        currentTarget = ResetTarget();
    }

    protected void OnEnable()
    {
        es.OnDeath += Death;
    }

    protected void OnDisable()
    {
        es.OnDeath -= Death;
    }

    protected void Update()
    {
        
    }

    protected void SetAttackDist(float attackDist)
    {
        this.attackDist = attackDist;
        attackDistSqr = attackDist * attackDist;
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    protected virtual void Attack()
    {
        transform.LookAt(currentTarget);
        StartCoroutine(AttackTimer());
    }

    protected Vector3 ResetTarget()
    {
        switch (MainTarget)
        {
            case TargetType.EnemyTarget:
                state = AIState.PursueTarget;
                return GameMasterBehavior.Instance.EnemyTargetPosition;
            case TargetType.Player:
                state = AIState.PursuePlayer;
                return GameMasterBehavior.Instance.playerObject.transform.position;
            default:
                goto case TargetType.EnemyTarget;
        }
    }

    protected IEnumerator AttackTimer()
    {
        state = AIState.Attacking;
        yield return new WaitForSeconds(timeBetweenAttack);
        switch (MainTarget)
        {
            case TargetType.EnemyTarget:
                state = AIState.PursueTarget;
                break;
            case TargetType.Player:
                state = AIState.PursuePlayer;
                break;
            default:
                goto case TargetType.EnemyTarget;
        }
    }
}

//I think in the future I want the enemies to not initially be aware of the player
public enum AIState : byte
{
    Idle,
    Attacking,
    PursueTarget,
    PursuePlayer,
    SeekingPlayer,
    Dying
}

public enum TargetType : byte
{
    EnemyTarget,
    Player
}
