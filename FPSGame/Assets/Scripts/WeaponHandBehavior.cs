using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandBehavior : MonoBehaviour
{
    public byte inventorySize = 3;

    public GameObject currentWeapon;
    public List<GameObject> inventory;

    public PlayerController playerCont;

    #region Monobehavior
    private void Start()
    {
        if(transform.GetChild(0) != null)
        {
            currentWeapon = transform.GetChild(0).gameObject;

            inventory.Add(currentWeapon);
        }
    }

    private void OnEnable()
    {
        PlayerController.OnDrop += DropWeapon;
    }

    private void OnDisable()
    {
        PlayerController.OnDrop -= DropWeapon;
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            Debug.Log("Scroll Up");
        }
        if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            Debug.Log("Scroll Down");
        }
    }
    #endregion

    public void PickUpWeapon(ref GameObject weapon)
    {
        if (inventory.Count < inventorySize)
        {
            if (currentWeapon != null)
            {
                currentWeapon.SetActive(false);
            }

            currentWeapon = weapon;
            inventory.Add(currentWeapon);
        }
        else
        {
            GameObject temp;

            currentWeapon.GetComponent<HeldWeapon>().DropWeapon();

            temp = currentWeapon;
            currentWeapon = weapon;
            inventory[inventory.IndexOf(temp)] = currentWeapon;
        }
    }


    private void DropWeapon()
    {
        if(currentWeapon != null)
        {
            inventory.Remove(currentWeapon);
        }
    }
    
    private void SwapCurrentWeapon()
    {

    }
}
