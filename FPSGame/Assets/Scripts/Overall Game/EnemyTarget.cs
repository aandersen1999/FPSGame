using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public float health = 1000;

    private void Awake()
    {
        GameMasterBehavior.Instance.EnemyTargetPosition = transform.position;
    }
}
