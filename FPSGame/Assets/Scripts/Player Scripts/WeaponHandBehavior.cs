using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandBehavior : MonoBehaviour
{
    public delegate void Action();
    public event Action ChangeGun;

    public byte inventorySize = 2;

    public GameObject currentWeapon;
    public List<GameObject> inventory;

    public Player currentPlayer;

    #region Monobehavior
    private void Start()
    {
        if(transform.childCount != 0)
        {
            for(int i = 0; i < transform.childCount; ++i)
            {
                PickUpWeapon(transform.GetChild(i).gameObject);
            }
        }
    }

    private void OnEnable()
    {
        EventManager.instance.PEC.OnPressDrop += DropWeapon;
        EventManager.instance.PEC.OnPressNext += NextWeapon;
    }

    private void OnDisable()
    {
        EventManager.instance.PEC.OnPressDrop -= DropWeapon;
        EventManager.instance.PEC.OnPressNext -= NextWeapon;
    }
    #endregion

    public void PickUpWeapon(GameObject weapon)
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

            currentWeapon.GetComponent<Weapon>().Drop();

            temp = currentWeapon;
            currentWeapon = weapon;
            inventory[inventory.IndexOf(temp)] = currentWeapon;
        }
        ChangeGun?.Invoke();
    }

    public void OnBreakWeapon()
    {
        GameObject storeCurrent = currentWeapon;

        if (inventory.Count > 1) { NextWeapon(); }
        inventory.Remove(storeCurrent);
        inventory.TrimExcess();
        ChangeGun?.Invoke();
    }

    private void DropWeapon()
    {
        if(currentWeapon != null)
        {
            GameObject storeCurrent = currentWeapon;

            if(inventory.Count > 1) { NextWeapon(); }
            inventory.Remove(storeCurrent);
            inventory.TrimExcess();
        }
        ChangeGun?.Invoke();
    }
    
    private void SwapCurrentWeapon(int index)
    {
        GameObject storeCurrent = currentWeapon;

        currentWeapon = inventory[index];

        storeCurrent.SetActive(false);
        currentWeapon.SetActive(true);

        ChangeGun?.Invoke();
    }

    private void NextWeapon()
    {
        if (inventory.Count != 0)
        {
            int nextWeaponIndex = inventory.IndexOf(currentWeapon) + 1;

            if (nextWeaponIndex >= inventory.Count) { SwapCurrentWeapon(0); }
            else { SwapCurrentWeapon(nextWeaponIndex); }
        }
        ChangeGun?.Invoke();
    }

    private void PrevWeapon()
    {
        if (inventory.Count != 0)
        {
            int prevWeaponIndex = inventory.IndexOf(currentWeapon) - 1;

            if (prevWeaponIndex < 0) { SwapCurrentWeapon(inventory.Count - 1); }
            else { SwapCurrentWeapon(prevWeaponIndex); }
        }
        ChangeGun?.Invoke();
    }
}
