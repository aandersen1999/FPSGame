using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Weapon that can shoot through multiple enemies
/// </summary>
public class RifleGun : WeaponGun
{
    
    public byte through;

    private readonly CompareDistance c = new CompareDistance();
    protected override void CheckHit()
    {
        RaycastHit[] hits = new RaycastHit[through];
        int numHit = Physics.RaycastNonAlloc(camTrans.position, GetBloom(camTrans), hits);
        numHit = Mathf.Min(numHit, through);
        Array.Sort(hits, 0, numHit, c);

        for(int i = 0; i < numHit; ++i)
        {
            if (hits[i].transform.TryGetComponent(out Hurtbox hb))
            {
                hb.Hurt(damage, knockBack);
            }
        }
        
    }
}

public class CompareDistance : IComparer
{
    public int Compare(object x, object y)
    {
        RaycastHit hitX = (RaycastHit)x;
        RaycastHit hitY = (RaycastHit)y;
        if(hitX.distance < hitY.distance)
        {
            return -1;
        }
        else if(hitX.distance == hitY.distance) 
        { 
            return 0; 
        }
        return 1;
    }
}