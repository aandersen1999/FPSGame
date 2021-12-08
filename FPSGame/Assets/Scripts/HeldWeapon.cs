using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldWeapon : MonoBehaviour
{
    public string weaponName = "Unknown";

    public int defaultDurability = 100;
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
        if (canFire)
        {
            if(clip > 0)
            {
                anim.SetTrigger("Fire");

                clip--;

                BreakWeapon();
                StartCoroutine(FireRateWait());
            }
        }
    }

    private void ReloadStart()
    {
        if(clip < clipSize)
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
        if(durability <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void BreakWeapon(int damage)
    {
        durability -= damage;
        if(durability <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FireRateWait()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}
