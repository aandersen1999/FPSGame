using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float damage = 1.0f;

    public float Hit()
    {
        Destroy(gameObject);
        return damage;
    }

    private void Start()
    {
        StartCoroutine(timer());
    }

    private void OnDisable()
    {
        StopCoroutine(timer());
    }

    private IEnumerator timer()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
