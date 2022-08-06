using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public List<GameObject> weaponList;

    public void Interact(ref WeaponHandBehavior wh)
    {
        int choice = Random.Range(0, weaponList.Count);
        GameObject createdWeapon = Instantiate(weaponList[choice], wh.transform);

        wh.PickUpWeapon(ref createdWeapon);
    }
}
