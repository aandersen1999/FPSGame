using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMass : MonoBehaviour
{
    public Transform playerTransform;

    private float yValue = -160;
    private float speed = 2.0f;

    private void Update()
    {
        yValue += speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x, yValue, playerTransform.position.z + 133);
    }
}
