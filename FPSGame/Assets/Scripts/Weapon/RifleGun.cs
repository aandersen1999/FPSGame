using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon that can shoot through multiple enemies
/// </summary>
public class RifleGun : WeaponGun
{
    public byte through = 2;

    protected override void CheckHit()
    {
        RaycastHit[] hits = new RaycastHit[through];
        int numHit = Physics.RaycastNonAlloc(camTrans.position, GetBloom(camTrans), hits);
        numHit = Mathf.Min(numHit, through);

        for(int i = 0; i < numHit; ++i)
        {
            if (hits[i].transform.TryGetComponent(out Hurtbox hb))
            {
                hb.Hurt(damage, knockBack);
            }
        }
        
    }
}
