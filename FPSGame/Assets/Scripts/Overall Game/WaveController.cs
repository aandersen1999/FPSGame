using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public static WaveController Instance;

    public List<GameObject> waveQueue;
    [SerializeField]
    public EnemyContainer enemyContainer;

    public bool activeSpawning = false;
    public int spawnAtATime = 1;

    private List<Spawner> spawners = new List<Spawner>();

    private int amountToSpawn;
    private int spawned;


    private int DecaySpawn = 15;
    private int DistSpawn = 10;
    private int ShatteredSpawn = 5;

    private const int spawnLimit = 20;

    private void Awake()
    {
        Instance = this;
        waveQueue = CreateWaveQueue();
    }

    private void OnEnable()
    {
        EventManager.StartWave += startWave;
        EventManager.StopWave += stopWave;
    }

    private void OnDisable()
    {
        EventManager.StartWave -= startWave;
        EventManager.StopWave -= stopWave;
    }

    private void startWave()
    {
        if(spawners == null)
        {
            Debug.LogError("No Spawners found");
            return;
        }

        spawned = 0;

        StartCoroutine(WaveLoop());
    }

    private void stopWave()
    {
        waveQueue.Clear();
        DecaySpawn += 5;
        DistSpawn += 5;
        ShatteredSpawn += 2;
        waveQueue = CreateWaveQueue();
        amountToSpawn = waveQueue.Count;
        StopAllCoroutines();
    }

    private List<GameObject> CreateWaveQueue()
    {
        List<GameObject> _waveQueue = new List<GameObject>();
        for(int i = 0; i < DecaySpawn; i++)
        {
            _waveQueue.Add(enemyContainer.Decay);
        }
        for(int i = 0; i < DistSpawn; i++)
        {
            _waveQueue.Add(enemyContainer.Distortion);
        }
        for(int i = 0; i < ShatteredSpawn; ++i)
        {
            _waveQueue.Add(enemyContainer.Shattered);
        }
        _waveQueue = ShuffleScript.Shuffle(_waveQueue);

        amountToSpawn = _waveQueue.Count;

        return _waveQueue;
    }

    public void AddSpawner(Spawner spawner)
        => spawners.Add(spawner);
         

    private IEnumerator WaveLoop()
    {
        activeSpawning = true;
        for(spawned = 0; spawned < amountToSpawn; )
        {
            for(int i = 0; i < spawnAtATime; ++i)
            {
                spawners[0].SpawnCreature(waveQueue[spawned++]);
                if(spawned >= amountToSpawn || GameMasterBehavior.Instance.totalEnemies >= spawnLimit)
                {
                    break;
                }
            }

            while (GameMasterBehavior.Instance.totalEnemies >= spawnLimit)
            {
                yield return new WaitForSeconds(1.0f);
            }
           
            yield return new WaitForSeconds(1.0f);
        }
        activeSpawning = false;
    }
}

[System.Serializable]
public struct EnemyContainer
{
    [SerializeField]
    private List<GameObject> decay;
    [SerializeField]
    private List<GameObject> distortion;
    public GameObject DistortionElite;
    [SerializeField]
    private List<GameObject> shattered;
    public GameObject ShatteredElite;
    [SerializeField]
    private List<GameObject> undergrowth;
    public GameObject UndergrowthElite;
    public GameObject Skitter;
    public GameObject Spectre;
    public GameObject Swamp;
    public GameObject WillOWisp;
    public GameObject Wraith;

    public GameObject Decay { get { return ReturnFromList(decay); } }
    public GameObject Distortion { get { return ReturnFromList(distortion); } }
    public GameObject Shattered { get { return ReturnFromList(shattered); } }
    public GameObject UnderGrowth { get { return ReturnFromList(undergrowth); } }

    private GameObject ReturnFromList(List<GameObject> list)
    {
        int value = UnityEngine.Random.Range(0, list.Count);
        return list[value];
    }
}

static class ShuffleScript
{
    private static System.Random rng = new System.Random();

    public static List<T> Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
        return list;
    }
}
