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

    private bool paused = false;
    public bool Paused { get { return paused; } }

    private WaveController wc;
    private bool gameOver = false;

    public Dictionary<AmmoType, short> Ammo = new Dictionary<AmmoType, short>();

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Instance = this;
        wc = GetComponent<WaveController>();

        foreach (AmmoType i in System.Enum.GetValues(typeof(AmmoType)))
        {
            Ammo[i] = short.MaxValue;
        }
    }

    private void OnEnable()
    {
        EventManager.EventShortTrigger += CheckForGameOver;
    }

    private void OnDisable()
    {
        EventManager.EventShortTrigger -= CheckForGameOver;
        
    }

    public void TriggerPause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            paused = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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
            //Application.Quit();
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



