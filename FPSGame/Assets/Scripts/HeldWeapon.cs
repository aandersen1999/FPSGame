using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldWeapon : MonoBehaviour
{
    #region stats variables
    public string weaponName = "Unknown";

    public int defaultDurability = 100;
    public byte clipSize = 17;

    public float fireRate = .5f;
    public float randomKickBack = 3f;
    public float muzzleFlashIntensity = 1.0f;
    public float damage = 1.0f;
    public float knockBack = 0.0f;
    #endregion

    public int durability = 100;
    public byte clip = 17;

    public WeaponState currentState = WeaponState.Idle;

    public GameObject dropWeapon;
    public Light muzzleFlash;

    private Animator anim;

    #region Monobehavior
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.instance.PEC.OnPressAttack += Attack;
        EventManager.instance.PEC.OnPressReload += ReloadStart;
        EventManager.instance.PEC.OnPressDrop += DropWeapon;

        currentState = WeaponState.Swapping;
        anim.SetTrigger("Swapped");
    }

    private void OnDisable()
    {
        EventManager.instance.PEC.OnPressAttack -= Attack;
        EventManager.instance.PEC.OnPressReload -= ReloadStart;
        EventManager.instance.PEC.OnPressDrop -= DropWeapon;
    }

    private void Update()
    {
        if(clip <= 0)
        {
            ReloadStart();
        }
    }
    #endregion

    public void CreateHeldWeapon(string _name, byte _clip, byte _clipSize, int _durability, int _defaultDurability)
    {
        weaponName = _name;
        clip = _clip;
        clipSize = _clipSize;
        durability = _durability;
        defaultDurability = _defaultDurability;
    }

    public void DropWeapon()
    {
        GameObject droppedWeapon = Instantiate(dropWeapon, GameMasterBehavior.GameMaster.playerObject.transform);

        droppedWeapon.GetComponent<InteractableWeapon>().CreateWeapon(weaponName, clip, clipSize, durability, defaultDurability);

        Destroy(gameObject);
    }

    private void Attack()
    {
        if(currentState == WeaponState.Idle)
        {
            if(clip > 0)
            {
                anim.SetTrigger("Fire");

                clip--;

                StartCoroutine(FireRateWait());
                StartCoroutine(MuzzleFlash());
                BreakWeapon();
            }
        }
    }

    private void ReloadStart()
    {
        if(clip < clipSize && currentState == WeaponState.Idle)
        {
            anim.SetTrigger("Reload");

            clip = 0;
            currentState = WeaponState.Reload;
        }
    }

    private void BreakWeapon()
    {
        durability--;
        if (durability == 0)
        {
            GameMasterBehavior.GameMaster.playerController.weaponHand.OnBreakWeapon();
            Destroy(gameObject);
        }
    }

    private void BreakWeapon(int damage)
    {
        durability -= damage;
        if (durability <= 0)
        {
            GameMasterBehavior.GameMaster.playerController.weaponHand.OnBreakWeapon();
            Destroy(gameObject);
        }
    }

    private void ReloadEnd()
    {
        clip = clipSize;
        currentState = WeaponState.Idle;
    }

    private void SwapAnimEnd() => currentState = WeaponState.Idle;

    private IEnumerator FireRateWait()
    {
        currentState = WeaponState.Fire;
        yield return new WaitForSeconds(fireRate);
        currentState = WeaponState.Idle;
    }

    private IEnumerator MuzzleFlash()
    {
        float lightDegrade = muzzleFlashIntensity / 10.0f;

        muzzleFlash.intensity = muzzleFlashIntensity;

        while (muzzleFlash.intensity > 0)
        {
            muzzleFlash.intensity -= lightDegrade;
            yield return new WaitForSeconds(.017f);
        }
    }
}


public enum WeaponState : byte
{
    Idle,
    Fire,
    Reload,
    Swapping
}

public class FireWeaponEventArgs : EventArgs
{
    public float Damage { get; set; }
    public float KnockBack { get; set; }
}