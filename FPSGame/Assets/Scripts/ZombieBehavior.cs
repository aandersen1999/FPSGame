using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public float health;

    public float walkSpeed = 3f;

    public float zombieAggression;
    public static float hordeAgression;

    private void Start()
    {
        health = Random.Range(10.0f, 100.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<HitboxBehavior>() != null)
        {
            TakeDamage(other.GetComponent<HitboxBehavior>().damage);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0) { Destroy(this.gameObject); }
    }
}
