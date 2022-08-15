using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shattered : MonoBehaviour
{
    public float speed = 5f;

    public GameObject projectile;
    public Transform projectileSpawner;
    public byte projPerVolley = 5;

    private EnemyState state = EnemyState.Pursuing;
    private bool volleyReady = true;
    private float retreatSpeed;

    private NavMeshAgent nma;
    private EnemyScript es;

    private Vector3 currentTarget;

    private const float attackDist = 20.0f;
    private const float retreatDist = 15.0f;
    private const float volleyChargeTime = 6.5f;
    private const float timeBetweenProj = .25f;
    private readonly float attackDistSqr = attackDist * attackDist;
    private readonly float retreatDistSqr = retreatDist * retreatDist;

    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        es = GetComponent<EnemyScript>();
        retreatSpeed = speed / 2;
    }

    private void Start()
    {
        nma.speed = speed;
        currentTarget = GameMasterBehavior.Instance.playerObject.transform.position;
        nma.stoppingDistance = attackDist;
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
                if(es.GetDistanceFromPlayerSqr() <= attackDistSqr)
                {
                    transform.LookAt(currentTarget);
                    nma.Move(transform.TransformDirection(Vector3.left) * retreatSpeed * Time.deltaTime);
                    if(es.GetDistanceFromPlayerSqr() <= retreatDistSqr)
                    {
                        nma.Move(transform.TransformDirection(Vector3.back) * retreatSpeed * Time.deltaTime);
                    }
                    if (volleyReady)
                    {
                        StartCoroutine(ThrowVolley());
                    }
                }
                nma.destination = currentTarget;
                break;
            default:
                break;
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private IEnumerator ThrowVolley()
    {
        StartCoroutine(RechargeVolley());
        for(int i = 0; i < projPerVolley; ++i)
        {
            GameObject instance = Instantiate(projectile, projectileSpawner.position, projectileSpawner.rotation);
            instance.transform.LookAt(currentTarget + Vector3.up);
            yield return new WaitForSeconds(timeBetweenProj);
        }
    }

    private IEnumerator RechargeVolley()
    {
        volleyReady = false;
        yield return new WaitForSeconds(volleyChargeTime);
        volleyReady = true;
    }
}
