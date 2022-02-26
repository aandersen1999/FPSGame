using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectreBehavior : MonoBehaviour
{
    private EnemyScript script;

    private void Awake()
    {
        script = GetComponent<EnemyScript>();
    }
}
