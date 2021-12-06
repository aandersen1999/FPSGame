using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public byte health = 1;

    public float movementSpeed = 3f;

    public static Transform playerPoistion;

    private void Start()
    {
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
       
    }
}
