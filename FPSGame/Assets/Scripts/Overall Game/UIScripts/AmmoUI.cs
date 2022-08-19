using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Text currentAmmo;
    public Text pool;

    private WeaponHandBehavior wh;
    private WeaponGun gunRef;

    private void OnEnable()
    {
        if(wh != null)
        {
            wh.ChangeGun += OnGunChange;
        }
        else if(GameMasterBehavior.Instance.playerController != null)
        {
            wh = GameMasterBehavior.Instance.playerController.weaponHand;
            wh.ChangeGun += OnGunChange;
            OnGunChange();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        wh.ChangeGun -= OnGunChange;
    }

    private void Update()
    {
        if(gunRef != null)
        {
            UpdateDisplay();
        }
    }

    private void OnGunChange()
    {
        if(wh.currentWeapon != null)
        {
            if(wh.currentWeapon.TryGetComponent(out WeaponGun wg))
            {
                gunRef = wg;
            }
            else
            {
                gunRef = null;
            }
        }
        else
        {
            gunRef = null;
        }
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if(gunRef != null)
        {
            currentAmmo.text = gunRef.clip.ToString();
            pool.text = GameMasterBehavior.Instance.Ammo[gunRef.ammoType].ToString();
        }
        else
        {
            currentAmmo.text = "0";
            pool.text = "0";
        }
    }
}
