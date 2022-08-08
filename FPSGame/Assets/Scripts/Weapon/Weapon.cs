using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName = "Unknown";
    public float damage = 20.0f;
    public float knockBack = 0.0f;

    public GameObject dropWeapon;

    protected Animator anim;
    protected Transform camTrans;

    #region Monobehavior
    protected void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected void Start()
    {
        camTrans = GameMasterBehavior.Instance.playerController.camTransform;
        if(dropWeapon == null)
        {
            Debug.LogError($"{this} does not have a weapon to drop!");
        }
    }

    protected void OnEnable()
    {
        EventManager.instance.PEC.OnPressAttack += Attack;
        EventManager.instance.PEC.OnPressDrop += Drop;
    }

    protected void OnDisable()
    {
        EventManager.instance.PEC.OnPressAttack -= Attack;
        EventManager.instance.PEC.OnPressDrop -= Drop;
    }

    protected void Update()
    {
        
    }
    #endregion

    /// <summary>
    /// Must be overridden
    /// </summary>
    protected virtual void Attack()
    {
    }

    /// <summary>
    /// Should be overridden, but maybe not
    /// </summary>
    protected virtual void Drop()
    {
        GameObject dropped = Instantiate(dropWeapon, GameMasterBehavior.Instance.playerObject.transform);
        dropped.GetComponent<InteractableWeapon>().CreateWeapon(name, 17, 17, 100, 100);
        Destroy(gameObject);
    }

    public virtual void CreateHeldWeapon(string name)
    {
        this.name = name;
    }

    public override string ToString()
        => weaponName;
}