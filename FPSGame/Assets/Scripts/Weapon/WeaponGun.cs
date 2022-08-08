using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : Weapon
{
    #region Stats variables
    public short clipSize = 10;
    public short clip = 10;

    //Will remove/replace later to be animation based
    public float fireRate = .5f;
    public float randomKickBack = 3.0f;
    public float muzzleFlashIntensity = 1.0f;

    public float bloom = 0.0f;
    public byte bulletsPerShot = 1;
    #endregion

    public AmmoType ammoType = AmmoType.C_Ammo;
    public Light muzzleFlash;

    private float lightDegrade;
    private GunState state = GunState.Idle;

    #region Monobehavior
    protected new void Awake()
    {
        base.Awake();
        lightDegrade = muzzleFlashIntensity / .15f;
    }

    protected new void Start()
    {
        base.Start();
        if(muzzleFlash == null)
        {
            Debug.LogError($"{this} does not have a muzzleFlash!");
        }
    }

    protected new void OnEnable()
    {
        base.OnEnable();
        EventManager.instance.PEC.OnPressReload += ReloadStart;
        state = GunState.Swapping;
        anim.SetTrigger("Swapped");
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        EventManager.instance.PEC.OnPressReload -= ReloadStart;
    }

    protected new void Update()
    {
        if(clip <= 0)
        {
            ReloadStart();
        }
    }
    #endregion

    protected override void Attack()
    {
        if(state == GunState.Idle && clip > 0)
        {
            
            anim.SetTrigger("Fire");

            clip--;

            StartCoroutine(FireRateWait());
            StartCoroutine(MuzzleFlash());

            for(byte i = 0; i < bulletsPerShot; i++)
            {
                CheckHit();
            }
        }
    }

    protected override void Drop()
    {
        GameObject dropped = Instantiate(dropWeapon, GameMasterBehavior.Instance.playerObject.transform);
        dropped.GetComponent<InteractableWeapon>().CreateWeapon(name, (byte)clip, (byte)clipSize, 100, 100);
        Destroy(gameObject);
    }

    protected virtual void ReloadStart()
    {
        if (CheckReload)
        {
            anim.SetTrigger("Reload");

            clip = 0;
            state = GunState.Reload;
        }
    }

    protected virtual void ReloadEnd()
    {
        short ammoToLoad = (short)Mathf.Min(clipSize, GameMasterBehavior.Instance.Ammo[ammoType]);
        //temporarily commented out
        //GameMasterBehavior.Instance.Ammo[ammoType] -= ammoToLoad;

        clip = ammoToLoad;
        state = GunState.Idle;
    }

    protected virtual void SwapAnimEnd() => state = GunState.Idle;

    public void CreateHeldWeapon(string name, byte clip)
    {
        base.CreateHeldWeapon(name);
        this.clip = clip;
    }

    private bool CheckReload 
        => clip < clipSize && state == GunState.Idle && GameMasterBehavior.Instance.Ammo[ammoType] > 0;


    private Vector3 GetBloom(Transform trans)
    {
        Transform dummy = trans;
        dummy.eulerAngles += new Vector3(Random.Range(-bloom, bloom), Random.Range(-bloom, bloom), 0.0f);
        Debug.DrawRay(dummy.position, dummy.forward * 10, Color.red, 5.0f);
        return dummy.forward;
    }

    private void CheckHit()
    {
        if (Physics.Raycast(camTrans.position, GetBloom(camTrans), out RaycastHit hit))
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

    //Will replace later to make it anim based
    private IEnumerator FireRateWait()
    {
        state = GunState.Fire;
        yield return new WaitForSeconds(fireRate);
        state = GunState.Idle;
    }

    private IEnumerator MuzzleFlash()
    {
        muzzleFlash.intensity = muzzleFlashIntensity;

        while(muzzleFlash.intensity > 0)
        {
            muzzleFlash.intensity -= lightDegrade * Time.deltaTime;
            yield return null;
        }
    }
}

public enum GunState : byte
{
    Idle,
    Fire,
    Reload,
    Swapping
}

public enum AmmoType : byte
{
    C_Ammo, L_Ammo,
    A_Rifle, B_Rifle,
    Shotgun,
    Sniper, AntiM_Sniper
}

public struct WeaponStatContainer
{
    public string name;
    public short clip;
    public short clipSize;
}