using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pistol : MonoBehaviour
{
    public int durability = 100;
    public byte clipSize = 17;
    public byte clip = 17;

    public float fireRate = .5f;
    public float randomKickBack = 3f;

    public GameObject dropWeapon;

    private bool canFire = true;

    private Animator anim;

    #region MonoBehavior
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerController.OnAttack += Attack;
        PlayerController.OnReload += ReloadStart;
        PlayerController.OnDrop += DropWeapon;
    }

    private void OnDisable()
    {
        PlayerController.OnAttack -= Attack;
        PlayerController.OnReload -= ReloadStart;
        PlayerController.OnDrop -= DropWeapon;
    }
    #endregion


    private void Attack()
    {
        if (clip > 0)
        {
            if (canFire)
            {
                anim.SetTrigger("Fire");
                clip--;
                BreakWeapon();

                //GameMasterBehavior.GameMaster.playerObject.GetComponent<PlayerController>().KickBack(randomKickBack);

                StartCoroutine(FireRateWait());
            }
        }
    }

    private void ReloadStart()
    {
        if (clip < clipSize)
        {
            anim.SetTrigger("Reload");
            
            clip = 0;
            canFire = false;
        }
    }

    private void ReloadEnd()
    {
        clip = clipSize;
        canFire = true;
    }


    private void BreakWeapon()
    {
        durability--;
        if(durability <= 0) { Destroy(gameObject); }
    }

    private void BreakWeapon(int damage)
    {
        durability -= damage;
        if (durability <= 0) { Destroy(gameObject); }
    }



    private void DropWeapon()
    {
        GameObject droppedWeapon = Instantiate(dropWeapon, GameMasterBehavior.GameMaster.playerObject.transform);
        droppedWeapon.transform.parent = null;
        droppedWeapon.transform.position = new Vector3(droppedWeapon.transform.position.x, .25f, droppedWeapon.transform.position.z);
        droppedWeapon.GetComponent<InteractablePistol>().CreateWeapon(clip, durability);

        Destroy(gameObject);
    }

    private IEnumerator FireRateWait()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}