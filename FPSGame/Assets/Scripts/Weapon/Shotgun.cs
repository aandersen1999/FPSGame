using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponGun
{
    private bool prematureEndReload = false;

    protected override void Attack()
    {
        if (state == GunState.Idle && clip > 0)
        {

            anim.SetTrigger("Fire");

            clip--;

            StartCoroutine(FireRateWait());
            StartCoroutine(MuzzleFlash());

            for (byte i = 0; i < bulletsPerShot; ++i)
            {
                CheckHit();
            }
        }
        else if (state == GunState.Reload && clip != 0)
        {
            prematureEndReload = true;
        }
    }

    protected override void ReloadStart()
    {
        if (CheckReload)
        {
            anim.SetTrigger("Reload");
            state = GunState.Reload;
        }
    }

    public void Reload()
    {
        --GameMasterBehavior.Instance.Ammo[ammoType];
        if(GameMasterBehavior.Instance.Ammo[ammoType] <= 0)
        {
            prematureEndReload = true;
        }
        if (++clip >= clipSize || prematureEndReload)
        {
            anim.SetTrigger("ReloadEnd");
            prematureEndReload = false;
        }
    }

    protected override void ReloadEnd()
    {
        state = GunState.Idle;
    }
}
