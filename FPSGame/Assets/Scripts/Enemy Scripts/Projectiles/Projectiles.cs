using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    //shall be unused for now
    public float health = 25.0f;
    public float damage = 8.0f;
    public float stdDeviation = 2.5f;
    public float speed = 10.0f;

    public bool heatSeeking = false;

    private float timer = 6.0f;

    private void Awake()
    {
        damage += Random.Range(-stdDeviation, stdDeviation);
    }

    private void Start()
    {
        StartCoroutine(DeathTime());
    }

    private void Update()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;
    }

    private IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
