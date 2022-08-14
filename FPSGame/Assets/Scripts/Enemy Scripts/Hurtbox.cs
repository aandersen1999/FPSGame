using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    
    public float damageModifier = 1.0f;
    public float knockBackModifier = 1.0f;

    private HurtBoxManager hbm;

    private void Awake()
    {
        Transform parentRef = transform.parent;
        while(parentRef.parent != null)
        {
            parentRef = parentRef.parent;
        }
        hbm = parentRef.GetComponent<HurtBoxManager>();
    }
    public void Hurt(float damage, float knockBack)
    {
        Debug.Log($"Took {damage * damageModifier}");
        hbm.SendDamage(damage * damageModifier, knockBack * knockBackModifier);
    }
}
