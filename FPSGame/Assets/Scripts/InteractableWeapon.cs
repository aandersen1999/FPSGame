using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWeapon : MonoBehaviour
{
    public string weaponName = "Unknown";

    public byte defaultClip = 17;
    public int defaultDurability = 100;

    public byte currentClip = 0;
    public int currentDurability = 100;

    public GameObject heldWeapon;

    private GameObject model;

    private float spinSpeed = 5f;

    private void Start()
    {
        model = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        model.transform.localEulerAngles += new Vector3(0, spinSpeed);
    }

    public void CreateWeapon(string name, byte clip, int durability)
    {
        weaponName = name;
        currentClip = clip;
        currentDurability = durability;
    }

    public void PickUpWeapon()
    {
        GameObject createdWeapon = Instantiate(heldWeapon, GameMasterBehavior.GameMaster.playerObject.GetComponent<PlayerController>().weaponHand.transform);

        createdWeapon.GetComponent<Weapon_Pistol>().CreateHeldWeapon(currentClip, currentDurability);
        Destroy(gameObject);
    }
}
