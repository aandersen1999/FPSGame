using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public List<GameObject> waveQueue;
    [SerializeField]
    public EnemyContainer enemyContainer;

    private List<Spawner> spawners;

    private int amountToSpawn;
    private int spawned;
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
