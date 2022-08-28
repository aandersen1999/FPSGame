using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : PickUp
{
    public ushort amount;

    public override string GetInteractText()
    {
        return $"{interactText} {amount} {objectName}";
    }

    public override void Interact()
    {
        GameMasterBehavior.Instance.playerController.AddSouls(amount);
        base.Interact();
    }
}
