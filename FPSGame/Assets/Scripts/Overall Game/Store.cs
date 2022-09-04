using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Interactable
{
    public List<GameObject> weaponList;

    public override void Interact()
    {
        int choice = Random.Range(0, weaponList.Count);
        GameObject createdWeapon = Instantiate(weaponList[choice], GameMasterBehavior.Instance.playerController.weaponHand.transform);

        GameMasterBehavior.Instance.playerController.weaponHand.PickUpWeapon(createdWeapon);
    }
}
