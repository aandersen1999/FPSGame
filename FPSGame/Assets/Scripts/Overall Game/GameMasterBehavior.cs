using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterBehavior : MonoBehaviour
{ 
    public delegate void PauseHandle(bool pause);
    public static event PauseHandle OnPause;

    public static GameMasterBehavior Instance { get; private set; }
    public static LayerMask ObjectLayer { get; private set; }
    public static LayerMask BoxLayer { get; private set; }
    public static LayerMask DefaultLayer { get; private set; }

    public GameObject playerObject;
    public Player playerController;
    public EnemyTarget enemyTarget;
    public Vector3 EnemyTargetPosition;
    public int waveNum = 1;

    [System.NonSerialized]
    public int totalEnemies = 0;

    private bool paused = false;
    public bool Paused { get { return paused; } }
    public bool canPause = true;

    private WaveController wc;
    private AudioSource Music;
    private bool gameOver = false;

    public Dictionary<AmmoType, short> Ammo = new Dictionary<AmmoType, short>();

    private void Awake()
    {
        Instance = this;
        wc = GetComponent<WaveController>();
        Music = GetComponent<AudioSource>();
        Music.ignoreListenerPause = true;

        foreach (AmmoType i in System.Enum.GetValues(typeof(AmmoType)))
        {
            Ammo[i] = 200;
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
        if (canPause)
        {
            if (!paused)
            {
                Music.Play();
                Time.timeScale = 0;
                paused = true;
                OnPause?.Invoke(paused);
            }
            else
            {
                Music.Stop();
                Time.timeScale = 1;
                paused = false;
                OnPause?.Invoke(paused);
            }
        }
    }

    private void Start()
    {
        ObjectLayer = LayerMask.GetMask("Object");
        BoxLayer = LayerMask.GetMask("Enemies");
        DefaultLayer = LayerMask.GetMask("Default");
    }

    private void Update()
    {
        if (playerObject != null)
        {
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            EventManager.instance.StartWaveTrigger();
        }

        if (EventManager.instance.waveActive)
        {
            if(!wc.activeSpawning && totalEnemies <= 0)
            {
                EventManager.instance.StopWaveTrigger();
                ++waveNum;
            }
        }
    }

    public void TriggerGameOver()
        => gameOver = true;

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void CheckForGameOver()
    {
        if (gameOver)
        {
            SceneManager.LoadScene(4);
        }
    }

   
}



