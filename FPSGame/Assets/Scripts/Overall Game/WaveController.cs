using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public static WaveController Instance;

    public List<GameObject> waveQueue;
    [SerializeField]
    public EnemyContainer enemyContainer;

    private List<Spawner> spawners = new List<Spawner>();

    private int amountToSpawn;
    private int spawned;

    public bool activeSpawning = false;
    public bool paused = false;

    private void Awake()
    {
        Instance = this;
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

        amountToSpawn = 10;
        spawned = 0;

        StartCoroutine(WaveLoop());
    }

    private void stopWave()
    {
        //Will add code later to support creating a list of enemies to spawn
        //For now, it will remain empty
        StopAllCoroutines();
    }

    private List<GameObject> CreateWaveQueue()
    {
        List<GameObject> _waveQueue = new List<GameObject>();

        return _waveQueue;
    }

    public void AddSpawner(Spawner spawner)
        => spawners.Add(spawner);
         

    private IEnumerator WaveLoop()
    {
        activeSpawning = true;
        for(spawned = 0; spawned <= amountToSpawn; spawned++)
        {
            if (!paused) { spawners[0].SpawnCreature(enemyContainer.Decay); }
           
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
        int value = Random.Range(0, list.Count);
        return list[value];
    }
}
