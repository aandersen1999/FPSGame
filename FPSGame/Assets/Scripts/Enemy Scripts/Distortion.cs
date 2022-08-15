using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Distortion : MonoBehaviour
{
    public float speed = 3.5f;
    public float attackTime = 1.0f;
    //private DistState distState = DistState.seekPlayer;
    private EnemyState state = EnemyState.Pursuing;

    private NavMeshAgent nma;
    private EnemyScript es;

    private Vector3 currentTarget;

    //Distances
    private const float attackDist = 1.8f;
    //Squares the distances for efficiency purposes
    private readonly float attackDistSqr = attackDist * attackDist;

    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        es = GetComponent<EnemyScript>();
    }

    private void Start()
    {
        nma.speed = speed;
        currentTarget = GameMasterBehavior.Instance.playerObject.transform.position;
    }

    private void OnEnable()
    {
        es.OnDeath += Death;
    }

    private void OnDisable()
    {
        es.OnDeath -= Death;
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Pursuing:
                currentTarget = GameMasterBehavior.Instance.playerObject.transform.position;
                if(es.GetDistanceFromTargetSqr(currentTarget) <= attackDistSqr)
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
        yield return new WaitForSeconds(attackTime);
        state = EnemyState.Pursuing;
    }
}

enum DistState
{
    seekPlayer,
    approachPlayer,

}