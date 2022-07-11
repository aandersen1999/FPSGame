using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool activeSpawner = true;
    public float rangeArea = 20.0f;

    private void Start()
    {
        WaveController.Instance.AddSpawner(this);
    }

    public void SpawnCreature(GameObject enemy)
        => Instantiate(enemy, 
           new Vector3(Random.Range(-rangeArea, rangeArea), transform.position.y, transform.position.z), 
           transform.rotation, null);

    public void toggleActiveSpawner(bool toggle)
        => activeSpawner = toggle;
    public void toggleActiveSpawner() 
        => activeSpawner = !activeSpawner;
}