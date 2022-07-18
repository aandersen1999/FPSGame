using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Decay : MonoBehaviour
{
    public float speed = 2;

    private NavMeshAgent nma;
    private EnemyScript es;

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
        nma.destination = GameMasterBehavior.Instance.EnemyTargetPosition;
        nma.speed = speed;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
