using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMass : MonoBehaviour
{
    public Transform playerTransform;

    public bool floatUp = true;

    private float yValue = -160;
    private float speed = 2.0f;
    private float zValue = 133;

    private void Start()
    {
        yValue = transform.position.y;
        zValue = transform.position.z;
    }

    private void Update()
    {
        if (floatUp) { yValue += speed * Time.deltaTime; }
        
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x, yValue, playerTransform.position.z + zValue);
    }
}
