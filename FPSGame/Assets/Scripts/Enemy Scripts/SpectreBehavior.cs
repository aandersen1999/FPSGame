using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectreBehavior : MonoBehaviour
{
    public float defaultMovementSpeed = 0.01f;
    private float movementSpeed;

    private EnemyScript script;

    #region Monobehavior
    private void Awake()
    {
        script = GetComponent<EnemyScript>();
    }
    private void OnEnable()
    {
        movementSpeed = defaultMovementSpeed * EnemyScript.hordeAgression;
    }

    private void Update()
    {
        transform.LookAt(EnemyScript.playerPosition + Vector3.down);
    }

    private void FixedUpdate()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * movementSpeed;
    }
    #endregion

}
