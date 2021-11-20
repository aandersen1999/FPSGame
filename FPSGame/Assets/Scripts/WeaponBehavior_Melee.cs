using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior_Melee : MonoBehaviour
{
    public string weaponName = "Unknown";

    public float staminaDrain = 0f;
    public float recoveryTime = 2f;
    public float damage = 0f;

    public bool breakable = true;
    public byte weaponIntegrity = 10;
    public byte weaponHealth = 10;
}
