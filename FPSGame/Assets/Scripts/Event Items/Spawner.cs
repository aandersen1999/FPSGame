using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerRefrence;
    
    private void Start()
    {
        if(GameMasterBehavior.GameMaster.playerObject == null)
        {
            Instantiate(playerRefrence);
        }
    }
}
