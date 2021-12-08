using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePistol : MonoBehaviour
{
    public string itemName = "Pistol";

    public byte defaultClip = 17;
    public int defaultDurability = 100;

    public byte currentClip = 17;
    public int currentDurability = 100;

    public GameObject heldItem;

    private GameObject childObject;

    private float spinSpeed = 5f;

    private void Start()
    {
        childObject = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        childObject.transform.localEulerAngles += new Vector3(0, spinSpeed);
    }


    public void CreateWeapon()
    {
        currentClip = defaultClip;
        currentDurability = defaultDurability;
    }

    public void CreateWeapon(byte clip, int durability)
    {
        currentClip = clip;
        currentDurability = durability;
    }
}
