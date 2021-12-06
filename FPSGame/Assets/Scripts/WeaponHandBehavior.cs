using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandBehavior : MonoBehaviour
{
    public byte inventorySize = 3;

    public GameObject currentWeapon;

    public PlayerController playerCont;

    private void Start()
    {
        currentWeapon = transform.GetChild(0).gameObject;
    }

    
}
