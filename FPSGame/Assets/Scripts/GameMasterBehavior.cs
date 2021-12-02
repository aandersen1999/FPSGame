using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterBehavior : MonoBehaviour
{
    public static GameMasterBehavior GameMaster { get; private set; }

    public GameObject playerObject;

    private void Awake()
    {
        GameMaster = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        
    }


}
