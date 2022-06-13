using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandBehavior : MonoBehaviour
{
    public byte inventorySize = 3;

    public GameObject currentWeapon;
    public List<GameObject> inventory;

    public Player currentPlayer;

    #region Monobehavior
    private void Start()
    {

    }

    private void OnEnable()
    {
        PlayerController.OnPressDrop += DropWeapon;
        PlayerController.OnPressNext += NextWeapon;
        PlayerController.OnPressPrev += PrevWeapon;
    }

    private void OnDisable()
    {
        PlayerController.OnPressDrop -= DropWeapon;
        PlayerController.OnPressNext -= NextWeapon;
        PlayerController.OnPressPrev -= PrevWeapon;
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

    public void OnBreakWeapon()
    {
        GameObject storeCurrent = currentWeapon;

        if (inventory.Count > 1) { NextWeapon(); }
        inventory.Remove(storeCurrent);
    }

    private void DropWeapon()
    {
        if(currentWeapon != null)
        {
            GameObject storeCurrent = currentWeapon;

            if(inventory.Count > 1) { NextWeapon(); }
            inventory.Remove(storeCurrent);
        }
    }
    
    private void SwapCurrentWeapon(int index)
    {
        GameObject storeCurrent = currentWeapon;

        currentWeapon = inventory[index];

        storeCurrent.SetActive(false);
        currentWeapon.SetActive(true);
    }

    private void NextWeapon()
    {
        if (inventory.Count != 0)
        {
            int nextWeaponIndex = inventory.IndexOf(currentWeapon) + 1;

            if (nextWeaponIndex >= inventory.Count) { SwapCurrentWeapon(0); }
            else { SwapCurrentWeapon(nextWeaponIndex); }
        }
    }

    private void PrevWeapon()
    {
        if (inventory.Count != 0)
        {
            int prevWeaponIndex = inventory.IndexOf(currentWeapon) - 1;

            if (prevWeaponIndex < 0) { SwapCurrentWeapon(inventory.Count - 1); }
            else { SwapCurrentWeapon(prevWeaponIndex); }
        }
    }
}
