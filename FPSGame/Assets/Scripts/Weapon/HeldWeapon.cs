using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldWeapon : MonoBehaviour
{
    public bool bloomOn = true;

    #region stats variables
    public string weaponName = "Unknown";

    public int defaultDurability = 100;
    public byte clipSize = 17;

    public float fireRate = .5f;
    public float randomKickBack = 3f;
    public float muzzleFlashIntensity = 1.0f;

    public float damage = 20.0f;
    public float knockBack = 0.0f;
    public float bloom = 0.0f;
    public byte bulletsPerShot = 1;
    #endregion

    public int durability = 100;
    public byte clip = 17;

    public D_WeaponState currentState = D_WeaponState.Idle;

    public GameObject dropWeapon;
    public Light muzzleFlash;

    private Animator anim;
    private Transform camTransRef;

    #region Monobehavior
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        camTransRef = GameMasterBehavior.Instance.playerController.camTransform;
        
    }

    private void OnEnable()
    {
        EventManager.instance.PEC.OnPressAttack += Attack;
        EventManager.instance.PEC.OnPressReload += ReloadStart;
        EventManager.instance.PEC.OnPressDrop += DropWeapon;

        currentState = D_WeaponState.Swapping;
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
        if(clip <= 0 && currentState == D_WeaponState.Idle)
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
        GameObject droppedWeapon = Instantiate(dropWeapon, GameMasterBehavior.Instance.playerObject.transform);

        droppedWeapon.GetComponent<InteractableWeapon>().CreateWeapon(weaponName, clip, clipSize, durability, defaultDurability);

        Destroy(gameObject);
    }

    private void Attack()
    {
        if(currentState == D_WeaponState.Idle)
        {
            if(clip > 0)
            {
                anim.SetTrigger("Fire");

                clip--;

                StartCoroutine(FireRateWait());
                StartCoroutine(MuzzleFlash());
                BreakWeapon();

                for(byte i = 0; i < bulletsPerShot; i++)
                {
                    if (Physics.Raycast(camTransRef.position, GetBloom(camTransRef), out RaycastHit hit))
                    {

                        if (hit.transform.parent != null)
                        {
                            if (hit.transform.parent.TryGetComponent(out EnemyScript inst))
                            {
                                inst.TakeDamage(damage);
                            }
                        }
                    }
                }
                
            }
        }
    }

    private void ReloadStart()
    {
        if(clip < clipSize && currentState == D_WeaponState.Idle)
        {
            anim.SetTrigger("Reload");

            clip = 0;
            currentState = D_WeaponState.Reload;
        }
    }

    private void BreakWeapon()
    {
        durability--;
        if (durability == 0)
        {
            GameMasterBehavior.Instance.playerController.weaponHand.OnBreakWeapon();
            Destroy(gameObject);
        }
    }

    private void BreakWeapon(int damage)
    {
        durability -= damage;
        if (durability <= 0)
        {
            GameMasterBehavior.Instance.playerController.weaponHand.OnBreakWeapon();
            Destroy(gameObject);
        }
    }

    private Vector3 GetBloom(Transform transRef)
    {
        Transform dummy = transRef;
        dummy.eulerAngles += new Vector3(UnityEngine.Random.Range(-bloom, bloom), UnityEngine.Random.Range(-bloom, bloom), 0.0f);
        Debug.DrawRay(dummy.position, dummy.forward * 10, Color.red, 5.0f);
        return dummy.forward;
        
    }

    private void ReloadEnd()
    {
        clip = clipSize;
        currentState = D_WeaponState.Idle;
    }

    private void SwapAnimEnd() => currentState = D_WeaponState.Idle;

    private IEnumerator FireRateWait()
    {
        currentState = D_WeaponState.Fire;
        yield return new WaitForSeconds(fireRate);
        currentState = D_WeaponState.Idle;
    }

    private IEnumerator MuzzleFlash()
    {
        float lightDegrade = muzzleFlashIntensity / .15f;

        muzzleFlash.intensity = muzzleFlashIntensity;

        while (muzzleFlash.intensity > 0)
        {
            muzzleFlash.intensity -= lightDegrade * Time.deltaTime;
            yield return null;
        }
    }
}


public enum D_WeaponState : byte
{
    Idle,
    Fire,
    Reload,
    Swapping
}