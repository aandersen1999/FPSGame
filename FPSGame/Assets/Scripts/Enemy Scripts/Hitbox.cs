using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float damage = 1.0f;

    private bool hit = false;

    public float Hit()
    {
        hit = true;
        return damage;
    }

    private void Start()
    {
        StartCoroutine(timer());
    }

    private void LateUpdate()
    {
        if (hit)
        {
            Destroy(gameObject);
        }
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
