using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Wraith : EnemyAI
{
    public Floater floater;
    public Transform meshTrans;

    private const float sinkDist = 3.5f;
    private const float sinkTime = 4.0f;
    private readonly float SinkDistPS = sinkDist / sinkTime;

    private bool setDestroy = false;

    private new void Awake()
    {
        base.Awake();
        meshTrans = floater.gameObject.transform;
    }

    private new void OnEnable()
    {
        base.OnEnable();
        EventManager.StopWave += Death;
    }

    private new void OnDisable()
    {
        base.OnDisable();
        EventManager.StopWave -= Death;
    }

    private new void Update()
    {
        switch (state)
        {
            case AIState.Idle:
                if (!nma.isStopped) { nma.isStopped = true; }
                break;
            case AIState.PursuePlayer:
                if(nma.isStopped) { nma.isStopped = false; }
                currentTarget = GameMasterBehavior.Instance.playerObject.transform.position;
                if(es.GetDistanceFromPlayerSqr() <= AttackDistSqr)
                {
                    Attack();
                }
                else
                {
                    nma.speed = speed;
                }
                nma.destination = currentTarget;
                break;
            case AIState.Dying:
               if(setDestroy) { Destroy(gameObject); }
                break;
            default:
                break;
        }
    }

    protected override void Attack()
    {
        base.Attack();
        nma.speed = 0;
        es.PutOutHitBox(0);
        StartCoroutine(AttackTimer());
    }

    protected override void Death()
    {
        nma.enabled = false;
        state = AIState.Dying;
        floater.enabled = false;
        StartCoroutine(sinkTimer());
    }

    private IEnumerator sinkTimer()
    {

        for (float sunk = 0.0f; sunk <= sinkDist; )
        {
            float distPerFrame = SinkDistPS * Time.deltaTime;
            meshTrans.localPosition += Vector3.down * distPerFrame;
            sunk += distPerFrame;
            yield return null;
        }
        setDestroy = true;
    }
}
