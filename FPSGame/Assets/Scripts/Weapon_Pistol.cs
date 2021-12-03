using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pistol : MonoBehaviour
{
    public byte totalAmmo = 100;
    public byte clipSize = 17;
    public byte clip = 17;

    public float fireRate = 1.0f;

    public GameObject dropWeapon;

    private bool canFire = true;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerController.OnAttack += Attack;
        PlayerController.OnReload += ReloadStart;
    }

    private void OnDisable()
    {
        PlayerController.OnAttack -= Attack;
        PlayerController.OnReload -= ReloadStart;
    }

    

    private void Attack()
    {
        if (clip > 0)
        {
            if (canFire)
            {

                anim.SetTrigger("Fire");
                clip--;

                StartCoroutine(FireRateWait());
            }
        }
    }

    private void ReloadStart()
    {
        if (clip < clipSize && totalAmmo > 0)
        {
            anim.SetTrigger("Reload");

            totalAmmo += clip;
            clip = 0;
            canFire = false;
        }
    }

    private void ReloadEnd()
    {
        byte clipAdd = (totalAmmo > clipSize) ? clipSize : totalAmmo;
        totalAmmo -= clipAdd;

        clip += clipAdd;
        canFire = true;
    }

    private IEnumerator FireRateWait()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}
