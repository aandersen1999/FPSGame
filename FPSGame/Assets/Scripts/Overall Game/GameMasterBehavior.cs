using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterBehavior : MonoBehaviour
{
    public static GameMasterBehavior Instance { get; private set; }
    public static LayerMask ObjectLayer { get; private set; }

    public GameObject playerObject;
    public Player playerController;
    public Vector3 EnemyTargetPosition;

    [System.NonSerialized]
    public int totalEnemies = 0;

    private WaveController wc;

    private void Awake()
    {
        Instance = this;
        wc = GetComponent<WaveController>();
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

        //Here for debug purposes
        if (Input.GetKeyDown(KeyCode.V))
        {
            EventManager.instance.StartWaveTrigger();
        }

        if (EventManager.instance.waveActive)
        {
            if(!wc.activeSpawning && totalEnemies <= 0)
            {
                EventManager.instance.StopWaveTrigger();
            }
        }
    }
}



