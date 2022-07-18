﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool active = true;
    public bool canTakeDamage = true;
    public bool takesKnockback = true;

    public float health = 100.0f;
    public float strength = 1.0f;
    public float poise = 10.0f;

    public static Vector3 playerPosition = new Vector3();
    public static float hordeAgression = 1.0f;

    public delegate void DeathAction();
    public event DeathAction OnDeath;


    #region Monobehavior
    private void OnEnable()
    {
        GameMasterBehavior.Instance.totalEnemies++;
    }

    private void OnDisable()
    {
        GameMasterBehavior.Instance.totalEnemies--;
    }

    private void Update()
    {
        
    }
    #endregion

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            health -= damage;

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
}
