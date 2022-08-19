using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In hindsight, it would've been better to not make this monobehavior
public class EnemyScript : MonoBehaviour
{
    public bool active = true;
    public bool canTakeDamage = true;
    public bool takesKnockback = true;

    public float health = 100.0f;
    public float strength = 1.0f;
    public float poise = 10.0f;

    public delegate void DeathAction();
    public event DeathAction OnDeath;

    public Color bloodColor;
    public ParticleSystem BloodSplatter;

    [SerializeField]
    private Vector3 hitboxSpawn = new Vector3(0, 1.0f, 1.6f);
    public List<GameObject> hitBoxes;

    private bool BloodLauncherExists = true;

    private void Awake()
    {
        if(BloodSplatter == null)
        {
            Debug.LogWarning($"{gameObject} has no blood particle system present!");
            BloodLauncherExists = false;
        }
        else
        {
            var main = BloodSplatter.main;
            main.startColor = bloodColor;
        }
    }

    #region Monobehavior
    private void OnEnable()
    {
        GameMasterBehavior.Instance.totalEnemies++;
    }

    private void OnDisable()
    {
        GameMasterBehavior.Instance.totalEnemies--;
    }
    #endregion

    //Returns the distance Squared
    public float GetDistanceFromPlayerSqr()
    {
        Vector3 reference = transform.position - GameMasterBehavior.Instance.playerObject.transform.position;
        return reference.sqrMagnitude;
    }

    public float GetDistanceFromTargetSqr(Vector3 target)
    {
        Vector3 reference = transform.position - target;
        return reference.sqrMagnitude;
    }

    public void PutOutHitBox(int hitID)
    {
        GameObject reference = Instantiate(hitBoxes[hitID], transform, false);
        reference.transform.localPosition = hitboxSpawn;
    }

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            EmitBlood((int)(damage / 2));

            if (health <= 0.0f)
            {
                if (OnDeath != null)
                {
                    OnDeath();
                }
                else
                {
                    Destroy(gameObject);
                    Debug.LogWarning("No event present for death: " + gameObject.ToString());
                }
            }
        }
        if (takesKnockback)
        {
            //Will add code later
        }
    }

    private void EmitBlood(int particles)
    {
        if (BloodLauncherExists)
        {
            BloodSplatter.Emit(particles);
        }
    }
}

public class EnemyLogicStats
{
    public bool awareOfPlayer;

    public EnemyLogicStats()
    {
        awareOfPlayer = false;
    }
}

public enum EnemyState : byte
{
    Idle,
    Pursuing,
    Action,
    Retreating
}