using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float speed = 90.0f;

    private float rotationRef = 0;

    private void Update()
    {
        rotationRef += speed * Time.deltaTime % 360;
        transform.localEulerAngles = Vector3.forward * rotationRef;
    }
}
