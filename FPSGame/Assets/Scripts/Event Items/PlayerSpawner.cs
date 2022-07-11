using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerRefrence;

    private void Start()
    {
        if (GameMasterBehavior.Instance.playerObject == null)
        {
            Instantiate(playerRefrence);
        }
    }
}
