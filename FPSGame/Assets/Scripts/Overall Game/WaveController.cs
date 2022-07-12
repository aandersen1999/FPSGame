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

    public void AddSpawner(Spawner spawner)
        => spawners.Add(spawner);
         

    private IEnumerator WaveLoop()
    {
        activeSpawning = true;
        for(spawned = 0; spawned <= amountToSpawn; spawned++)
        {
            spawners[0].SpawnCreature(enemyContainer.Spectre);
            yield return new WaitForSeconds(1.0f);
        }
        activeSpawning = false;
    }
}

[System.Serializable]
public struct EnemyContainer
{
    public List<GameObject> Decay;
    public List<GameObject> Distortion;
    public GameObject DistortionElite;
    public List<GameObject> Shattered;
    public GameObject ShatteredElite;
    public List<GameObject> Undergrowth;
    public GameObject UndergrowthElite;
    public GameObject Skitter;
    public GameObject Spectre;
    public GameObject Swamp;
    public GameObject WillOWisp;
    public GameObject Wraith;
}
