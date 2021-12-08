using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxBehavior : MonoBehaviour
{
    public float damage;

    private byte frames = 13;

    private IEnumerator coroutine;

    private void Start()
    {
        StartCoroutine(DestroyTime(frames));
    }

    private IEnumerator DestroyTime(byte frames)
    {
        
        float seconds = frames * .016f;

        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }
}
