using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior_Melee : MonoBehaviour
{
    private Animator anim;

    public string weaponName = "Unknown";

    public float staminaDrain = 0f;
    public float recoveryTime = 1f;
    public float damage = 0f;

    public bool breakable = true;
    public byte weaponIntegrity = 10;
    public byte weaponHealth = 10;

    public PlayerController playerController;
    public GameObject meleeHitbox;

    private bool activeHitbox;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void attack()
    {
        anim.SetTrigger("doAttack");

        GameObject hitbox = Instantiate(meleeHitbox, this.transform);
        hitbox.GetComponent<HitboxBehavior>().damage = damage;

        playerController.stamina -= staminaDrain;
    }

}
