using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Decay : MonoBehaviour
{
    public float speed = 2;

    private NavMeshAgent nma;
    private EnemyScript es;

    private Vector3 currentTarget;

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
        nma.destination = currentTarget;
    }

    private void Update()
    {
        if(es.GetDistanceFromPlayer() <= 6.0f)
        {
            currentTarget = GameMasterBehavior.Instance.playerObject.transform.position;
        }
        else
        {
            currentTarget = GameMasterBehavior.Instance.EnemyTargetPosition;
        }

        
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
