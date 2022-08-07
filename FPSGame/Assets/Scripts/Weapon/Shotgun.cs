using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    Weapon test;

    private void Start()
    {
        test = GetComponent<Weapon>();
        if(test == null)
        {
            Debug.Log("Fail");
        }
    }
}
