using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public float health = 200;

    private void Awake()
    {
        GameMasterBehavior.Instance.EnemyTargetPosition = transform.position;
        GameMasterBehavior.Instance.enemyTarget = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Hitbox>() != null)
        {
            Hitbox hb = other.GetComponent<Hitbox>();

            TakeDamage(hb.Hit());
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
            GameMasterBehavior.Instance.TriggerGameOver();
        }
    }
}
