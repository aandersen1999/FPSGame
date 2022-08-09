using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : PickUp
{
    public short clip = 0;

    public GameObject weapon;

    #region monobehavior
    protected new void Awake()
    {
        base.Awake();
    }

    protected new void Start()
    {
        base.Start();
    }

    protected new void Update()
    {
        base.Update();
    }
    #endregion

    public override void Interact()
    {
        GameObject weaponObject = Instantiate(weapon, GameMasterBehavior.Instance.playerController.weaponHand.gameObject.transform);
        WeaponArgs args = new WeaponArgs();
        args.name = objectName;
        args.clip = clip;

        weaponObject.GetComponent<Weapon>().CreateHeldWeapon(args);

        GameMasterBehavior.Instance.playerController.weaponHand.PickUpWeapon(ref weaponObject);
        base.Interact();
    }

    public void Create(WeaponArgs args)
    {
        objectName = args.name;
        clip = args.clip;
    }
}
