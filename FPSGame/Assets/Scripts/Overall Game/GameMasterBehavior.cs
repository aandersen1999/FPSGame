using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterBehavior : MonoBehaviour
{
    public static GameMasterBehavior Instance { get; private set; }
    public static LayerMask ObjectLayer { get; private set; }

    public GameObject playerObject;
    public Player playerController;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ObjectLayer = LayerMask.GetMask("Object");
    }

    private void Update()
    {
        if (playerObject != null)
        {
            EnemyScript.playerPosition = playerObject.transform.position + Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            EventManager.instance.StartWaveTrigger();
        }
    }
}



