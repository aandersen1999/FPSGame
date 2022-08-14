using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBoxManager : MonoBehaviour
{
    private EnemyScript es;

    private void Awake()
    {
        es = GetComponent<EnemyScript>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void SendDamage(float damage, float knockBack)
    {
        es.TakeDamage(damage);
    }
}
