using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    private const float Speed = .135f;
    private void Update()
    {
        transform.position += (transform.forward * Speed) * Time.deltaTime;
    }

}
