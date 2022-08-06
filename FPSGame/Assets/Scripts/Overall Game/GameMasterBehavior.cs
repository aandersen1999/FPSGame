using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool gameOver = false;

    private void Awake()
    {
        Instance = this;
        wc = GetComponent<WaveController>();
    }

    private void OnEnable()
    {
        EventManager.EventShortTrigger += CheckForGameOver;
    }

    private void OnDisable()
    {
        EventManager.EventShortTrigger -= CheckForGameOver;
    }

    private void Start()
    {
        ObjectLayer = LayerMask.GetMask("Object");
    }

    private void Update()
    {
        if (playerObject != null)
        {
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (EventManager.instance.waveActive)
        {
            if(!wc.activeSpawning && totalEnemies <= 0)
            {
                EventManager.instance.StopWaveTrigger();
            }
        }
    }

    public void TriggerGameOver()
        => gameOver = true;

    private void CheckForGameOver()
    {
        if (gameOver)
        {
            SceneManager.LoadScene(4);
        }
    }
}



