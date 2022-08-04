using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWeapon : MonoBehaviour
{
    public string weaponName = "Unknown";

    public int defaultDurabilty = 100;
    public int durabilty = 100;

    public byte clip = 17;
    public byte clipSize = 17;

    public GameObject heldWeapon;

    private GameObject model;

    private float spinSpeed = 5f;

    private void Start()
    {
        model = transform.GetChild(0).gameObject;
        float value;

        if(transform.parent != null)
        {
            transform.parent = null;
        }
        if(ReturnY(out value))
        {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        model.transform.localEulerAngles += new Vector3(0, spinSpeed);
    }

    public void CreateWeapon(string _name, byte _clip, byte _clipSize, int _durability, int _defaultDurability)
    {
        weaponName = _name;
        clip = _clip;
        clipSize = _clipSize;
        durabilty = _durability;
        defaultDurabilty = _defaultDurability;
    }

    public void PickUpWeapon()
    {
        GameObject createdWeapon = Instantiate(heldWeapon, GameMasterBehavior.Instance.playerController.weaponHand.gameObject.transform);

        createdWeapon.GetComponent<HeldWeapon>().CreateHeldWeapon(weaponName, clip, clipSize, durabilty, defaultDurabilty);

        GameMasterBehavior.Instance.playerController.weaponHand.PickUpWeapon(ref createdWeapon);
        Destroy(gameObject);
    }

    private bool ReturnY(out float value)
    {
        RaycastHit hit;
        value = 0f;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            value = hit.point.y;
            return true;
        }
        return false;
    }
}
