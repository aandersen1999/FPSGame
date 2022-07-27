using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Decay : MonoBehaviour
{
    public float speed = 2;
    public EnemyState state = EnemyState.Pursuing;

    private NavMeshAgent nma;
    private EnemyScript es;

    private Vector3 currentTarget;

    //Distances
    private const float dfp = 6.0f;
    private const float attackDist = 1.8f;
    //Squares the distances for efficiency purposes
    private readonly float dfpSqr = dfp * dfp;
    private readonly float attackDistSqr = attackDist * attackDist;

    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        es = GetComponent<EnemyScript>();
    }

    private void OnEnable()
    {
        es.OnDeath += Death;
    }

    private void OnDisable()
    {
        es.OnDeath -= Death;
    }

    private void Start()
    {
        nma.speed = speed;
        currentTarget = GameMasterBehavior.Instance.EnemyTargetPosition;
        
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Pursuing:
                currentTarget = (es.GetDistanceFromPlayerSqr() <= dfpSqr) ? GameMasterBehavior.Instance.playerObject.transform.position
                                                                    : GameMasterBehavior.Instance.EnemyTargetPosition;

                if (es.GetDistanceFromTargetSqr(currentTarget) <= attackDistSqr)
                {
                    nma.speed = 0;
                    es.PutOutHitBox(0);
                    StartCoroutine(AttackTimer());
                }
                else
                {
                    nma.speed = speed;
                }

                nma.destination = currentTarget;
                break;
            case EnemyState.Action:
                break;
            default:
                break;
        }
        

    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private IEnumerator AttackTimer()
    {
        state = EnemyState.Action;
        yield return new WaitForSeconds(2.0f);
        state = EnemyState.Pursuing;
    }
}
