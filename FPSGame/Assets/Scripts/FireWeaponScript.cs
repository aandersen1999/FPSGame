using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponScript : MonoBehaviour
{
    public byte bullets = 1;
    public float damage = 40.0f;
    public float bloom = 0.0f;

    private Vector3 bulletSpawn;

    private void Update()
    {
        if(GameMasterBehavior.GameMaster.playerController != null)
        {
            bulletSpawn = GameMasterBehavior.GameMaster.playerController.playerCamera.transform.position;
        }
    }
}
