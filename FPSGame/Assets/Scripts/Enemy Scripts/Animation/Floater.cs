using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public float ampli = 1.0f;
    public float freq = 1.0f;

    private float defaultY;

    private void Start()
    {
        defaultY = ampli;
    }

    private void Update()
    {
        float yVal = Mathf.Sin(Time.time * freq) * ampli + defaultY;

        transform.localPosition = Vector3.up * yVal;
    }
}
